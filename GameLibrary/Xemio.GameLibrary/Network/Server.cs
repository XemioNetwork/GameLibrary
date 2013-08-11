using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
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
    public class Server : IServer, IGameHandler
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

            this.Connections = new List<IConnection>();

            this._subscribers = new List<IServerLogic>();
            this._sender = new PackageSender();

            this.Subscribe(new TimeSyncServerLogic());
            this.StartServerLoop();

            GameLoop loop = XGL.Components.Get<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Fields
        private readonly List<IServerLogic> _subscribers;
        private readonly PackageSender _sender;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Server"/> is active.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Gets the event manager.
        /// </summary>
        protected EventManager EventManager
        {
            get { return XGL.Components.Get<EventManager>(); }
        }
        #endregion

        #region Event Methods
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

            this.EventManager.Publish(new ReceivedPackageEvent(package, connection));
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

            this.EventManager.Publish(new SentPackageEvent(package, connection));
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected virtual void OnClientJoined(IConnection connection)
        {
            foreach (IServerLogic subscriber in this._subscribers)
            {
                subscriber.OnClientJoined(this, connection);
            }

            this.EventManager.Publish(new ClientJoinedEvent(connection));
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected virtual void OnClientLeft(IConnection connection)
        {
            foreach (IServerLogic subscriber in this._subscribers)
            {
                subscriber.OnClientLeft(this, connection);
            }

            this.EventManager.Publish(new ClientLeftEvent(connection));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Accepts the connection.
        /// </summary>
        protected virtual IConnection AcceptConnection()
        {
            return this.Protocol.AcceptConnection();
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
                IConnection connection = this.AcceptConnection();

                this.Connections.Add(connection);
                this.StartListening(connection);

                this.OnClientJoined(connection);
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
                    this.EventManager.Publish(new ExceptionEvent(ex));

                    this.Connections.Remove(connection);
                    this.OnClientLeft(connection);

                    break;
                }
            }
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
                subscriber.Tick(this, elapsed);
            }
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public void Render()
        {
        }
        #endregion

        #region Implementation of IServer
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public List<IConnection> Connections { get; private set; }
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
            ThreadInvoker invoker = XGL.Components.Get<ThreadInvoker>();

            this.OnBeginSendPackage(package, receiver);
            receiver.Send(package);
            this.OnSentPackage(package, receiver);
        }
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
        #endregion
    }
}
