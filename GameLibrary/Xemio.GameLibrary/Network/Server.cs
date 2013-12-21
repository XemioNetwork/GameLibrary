using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
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
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Server" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public Server(string url)
        {
            this._connections = new CachedList<IConnection>();

            this._subscribers = new List<IServerHandler>();

            this.Active = true;

            this.Protocol = ProtocolFactory.CreateServerProtocol(url);
            this.Protocol.Server = this;

            this.Subscribe(new TimeSyncServerHandler(this));

            this._packageQueue = new PackageQueue();
            this._packageQueue.Start();

            this._connectionManager = new ServerConnectionManager(this);
            this._connectionManager.Start();
        }
        #endregion

        #region Fields
        private readonly PackageQueue _packageQueue;
        private readonly ServerConnectionManager _connectionManager;

        private readonly List<IServerHandler> _subscribers;
        private readonly CachedList<IConnection> _connections;
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

        #region Methods
        /// <summary>
        /// Called when the server received a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        protected internal virtual void OnReceivePackage(Package package, IConnection connection)
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
        protected virtual void OnBeginSendPackage(Package package, IConnection connection)
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
        protected virtual void OnSentPackage(Package package, IConnection connection)
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
        protected internal virtual void OnClientJoined(IConnection connection)
        {
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
        protected internal virtual void OnClientLeft(IConnection connection)
        {
            foreach (IServerHandler subscriber in this._subscribers)
            {
                subscriber.OnClientLeft(this, connection);
            }

            this.EventManager.Publish(new ClientLeftEvent(connection));
        }
        /// <summary>
        /// Accepts the connection.
        /// </summary>
        protected internal virtual IConnection AcceptConnection()
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
        /// Gets the protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public IList<IConnection> Connections
        {
            get { return this._connections; }
        }
        /// <summary>
        /// Sends the specified package to all clients.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            using (this._connections.StartCaching())
            {
                foreach (IConnection connection in this._connections)
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
            this.OnBeginSendPackage(package, receiver);
            this._packageQueue.Offer(package, receiver);
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
            this._packageQueue.Interrupt();

            this.Protocol.Close();
        }
        #endregion
    }
}
