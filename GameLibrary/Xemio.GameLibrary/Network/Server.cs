using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Internal;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Game;

namespace Xemio.GameLibrary.Network
{
    public class Server : IServer, IComponent
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Server" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public Server(string url)
        {
            logger.Info("Creating server for [{0}].", url);

            this.Connected = true;

            this._subscribers = new List<IServerHandler>();
            
            this.Protocol = ProtocolFactory.CreateServerProtocol(url);
            this.Protocol.Server = this;
            
            this._connectionManager = new ServerConnectionManager(this);
            this._connectionManager.Start();

            this.Subscribe(new TimeSyncServerHandler(this));
        }
        #endregion

        #region Fields
        private readonly ServerConnectionManager _connectionManager;
        private readonly List<IServerHandler> _subscribers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the event manager.
        /// </summary>
        protected EventManager EventManager
        {
            get { return XGL.Components.Get<EventManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the server received a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnReceivePackage(Package package, IConnection connection)
        {
            IEnumerable<IServerHandler> subscribers = this.GetSubscribers(package);
            foreach (IServerHandler subscriber in subscribers)
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
        public virtual void OnBeginSendPackage(Package package, IConnection connection)
        {
            IEnumerable<IServerHandler> subscribers = this.GetSubscribers(package);
            foreach (IServerHandler subscriber in subscribers)
            {
                subscriber.OnBeginSend(this, package, connection);
            }
        }
        /// <summary>
        /// Called when server sent a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnSentPackage(Package package, IConnection connection)
        {
            IEnumerable<IServerHandler> subscribers = this.GetSubscribers(package);
            foreach (IServerHandler subscriber in subscribers)
            {
                subscriber.OnSent(this, package, connection);
            }

            this.EventManager.Publish(new SentPackageEvent(package, connection));
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientJoined(IConnection connection)
        {
            logger.Info("Client {0} joined.", connection.Address);

            foreach (IServerHandler subscriber in this._subscribers)
            {
                subscriber.OnClientJoined(this, connection);
            }

            this.EventManager.Publish(new ClientJoinedEvent(connection));
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientLeft(IConnection connection)
        {
            logger.Info("Client {0} disconnected.", connection.Address);

            foreach (IServerHandler subscriber in this._subscribers)
            {
                subscriber.OnClientLeft(this, connection);
            }

            this.EventManager.Publish(new ClientLeftEvent(connection));
        }
        /// <summary>
        /// Accepts the connection.
        /// </summary>
        public virtual IConnection AcceptConnection()
        {
            return this.Protocol.AcceptConnection();
        }
        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <param name="package">The package.</param>
        private IEnumerable<IServerHandler> GetSubscribers(Package package)
        {
            return this._subscribers.Where(s => s.PackageType.IsInstanceOfType(package));
        }
        #endregion
        
        #region Implementation of IServer
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public IList<IConnection> Connections
        {
            get { return this._connectionManager.Connections; }
        }
        /// <summary>
        /// Gets a value indicating whether the server is alive.
        /// </summary>
        public bool Connected { get; private set; }
        /// <summary>
        /// Sends the specified package to all clients.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            var cachedList = (CachedList<IConnection>)this.Connections;

            using (cachedList.StartCaching())
            {
                foreach (IConnection connection in this.Connections)
                {
                    this.Send(package, connection);
                }
            }
        }
        /// <summary>
        /// Sends the specified package to the specified receiver.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void Send(Package package, IConnection receiver)
        {
            logger.Trace("Sending {0} to {1}.", package.GetType().Name, receiver.Address);

            this.OnBeginSendPackage(package, receiver);
            this._connectionManager.GetOutputQueue(receiver).Enqueue(package);
            this.OnSentPackage(package, receiver);
        }
        /// <summary>
        /// Subscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IServerHandler subscriber)
        {
            this._subscribers.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IServerHandler subscriber)
        {
            this._subscribers.Remove(subscriber);
        }
        /// <summary>
        /// Stops the server.
        /// </summary>
        public void Close()
        {
            this._connectionManager.Interrupt();

            this.Protocol.Close();
            this.Connected = false;
        }
        #endregion
    }
}
