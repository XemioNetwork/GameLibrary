﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Logic;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Game;

namespace Xemio.GameLibrary.Network
{
    public class Server : IComponent, IGameHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public Server(IServerProtocol protocol)
        {            
            this.Active = true;

            this.Protocol = protocol;
            this.Protocol.Server = this;

            this.Serializer = new PackageSerializer();
            this.Connections = new List<IConnection>();

            this._eventManager = XGL.Components.Get<EventManager>();
            this._subscribers = new List<IServerLogic>();

            this.Subscribe(new TimeSyncServerLogic());

            this.StartServerLoop();
            this.ProvideComponent();

            GameLoop loop = XGL.Components.Get<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Fields
        private readonly EventManager _eventManager;
        private readonly List<IServerLogic> _subscribers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Server"/> is active.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the package manager.
        /// </summary>
        public PackageSerializer Serializer { get; private set; }
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public List<IConnection> Connections { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IServerLogic subscriber)
        {
            this._subscribers.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IServerLogic subscriber)
        {
            this._subscribers.Remove(subscriber);
        }
        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <param name="package">The package.</param>
        private IEnumerable<IServerLogic> GetSubscribers(Package package)
        {
            return this._subscribers.Where(s => s.Type.IsInstanceOfType(package));
        }
        /// <summary>
        /// Provides the component.
        /// </summary>
        public void ProvideComponent()
        {
            XGL.Components.Add(new ValueProvider<Server>(this));
        }
        /// <summary>
        /// Called when the server received a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        protected virtual void OnReceivedPackage(Package package, IConnection connection)
        {
            IEnumerable<IServerLogic> subscribers = this.GetSubscribers(package);
            foreach (IServerLogic subscriber in subscribers)
            {
                subscriber.OnReceive(this, package, connection);    
            }

            this._eventManager.Publish(new ReceivedPackageEvent(package, connection));
        }
        /// <summary>
        /// Called when server is sending a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        protected virtual void OnBeginSendPackage(Package package, IConnection connection)
        {
            IEnumerable<IServerLogic> subscribers = this.GetSubscribers(package);
            foreach (IServerLogic subscriber in subscribers)
            {
                subscriber.OnBeginSend(this, package, connection);
            }
        }
        /// <summary>
        /// Called when server sent a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        protected virtual void OnSentPackage(Package package, IConnection connection)
        {
            IEnumerable<IServerLogic> subscribers = this.GetSubscribers(package);
            foreach (IServerLogic subscriber in subscribers)
            {
                subscriber.OnSent(this, package, connection);
            }

            this._eventManager.Publish(new SentPackageEvent(package, connection));
        }
        /// <summary>
        /// Starts the server loop.
        /// </summary>
        private void StartServerLoop()
        {
            Task.Factory.StartNew(this.ServerLoop);
        }
        /// <summary>
        /// Accepts incoming client connection requests.
        /// </summary>
        private void ServerLoop()
        {
            while (this.Active)
            {
                IConnection connection = this.Protocol.AcceptConnection();
                this._eventManager.Publish(new ClientJoinedEvent(connection));

                this.Connections.Add(connection);
                this.StartListening(connection);
            }
        }
        /// <summary>
        /// Starts listening to the connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void StartListening(IConnection connection)
        {
            Task.Factory.StartNew(() => this.Listen(connection));
        }
        /// <summary>
        /// Listens to the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void Listen(IConnection connection)
        {
            while (connection.Connected)
            {
                try
                {
                    Package package = connection.Receive();
                    if (package != null)
                    {
                        this.OnReceivedPackage(package, connection);
                    }
                }
                catch (Exception ex)
                {
                    this._eventManager.Publish(new ExceptionEvent(ex));

                    this.Connections.Remove(connection);
                    this._eventManager.Publish(new ClientLeftEvent(connection));

                    break;
                }
            }
        }
        /// <summary>
        /// Sends the specified package to all clients.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            for (int i = 0; i < this.Connections.Count; i++)
            {
                this.Send(package, this.Connections[i]);
            }
        }
        /// <summary>
        /// Sends the specified package to the specified receiver.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void Send(Package package, IConnection receiver)
        {
            this.OnBeginSendPackage(package, receiver);

            this.Protocol.Send(package, receiver);
            this.OnSentPackage(package, receiver);
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            foreach (IServerLogic subscriber in this._subscribers)
            {
                subscriber.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public void Render()
        {
        }
        #endregion
    }
}
