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
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Events.Server;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Intercetors;
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

            this._handlers = new List<IServerHandler>();
            this._interceptors = new List<IServerInterceptor>();
            
            this.Protocol = ProtocolFactory.CreateServerProtocol(url);
            this.Protocol.Server = this;
            
            this._connectionManager = new ServerConnectionManager(this);
            this._connectionManager.Start();

            this.Subscribe(new TimeSyncServerHandler(this));
        }
        #endregion

        #region Fields
        private readonly ServerConnectionManager _connectionManager;

        private readonly List<IServerHandler> _handlers;
        private readonly List<IServerInterceptor> _interceptors; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the event manager.
        /// </summary>
        protected IEventManager EventManager
        {
            get { return XGL.Components.Get<IEventManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <param name="handlers">The handlers.</param>
        /// <param name="interceptorAction">The interceptor action.</param>
        /// <param name="handlerAction">The handler action.</param>
        private bool HandleEvent(IInterceptableEvent evt, IEnumerable<IServerHandler> handlers, Action<IServerInterceptor> interceptorAction, Action<IServerHandler> handlerAction)
        {
            foreach (IServerInterceptor interceptor in this._interceptors)
            {
                interceptorAction(interceptor);
            }

            if (!evt.IsCanceled)
            {
                foreach (IServerHandler subscriber in handlers)
                {
                    handlerAction(subscriber);
                }

                this.EventManager.Publish(evt);
            }

            return !evt.IsCanceled;
        }
        /// <summary>
        /// Called when the server received a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnReceivePackage(Package package, IServerConnection connection)
        {
            var evt = new ServerReceivedPackageEvent(this, package, connection);

            this.HandleEvent(
                evt, this.GetSubscribers(package),
                interceptor => interceptor.InterceptReceived(evt),
                subscriber => subscriber.OnReceive(this, package, connection));
        }
        /// <summary>
        /// Called when server is sending a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public virtual bool OnBeginSendPackage(Package package, IServerConnection connection)
        {
            var evt = new ServerSendingPackageEvent(this, package, connection);

            return this.HandleEvent(
                evt, this.GetSubscribers(package),
                interceptor => interceptor.InterceptBeginSend(evt),
                subscriber => subscriber.OnBeginSend(this, package, connection));
        }
        /// <summary>
        /// Called when server sent a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnSentPackage(Package package, IServerConnection connection)
        {
            var evt = new ServerSentPackageEvent(this, package, connection);

            this.HandleEvent(
                evt, this.GetSubscribers(package),
                interceptor => interceptor.InterceptSent(evt),
                subscriber => subscriber.OnSent(this, package, connection));
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public virtual bool OnClientJoined(IServerConnection connection)
        {
            logger.Info("Client {0} joined.", connection.Address);

            var evt = new ClientJoinedEvent(this, connection);

            return this.HandleEvent(
                evt, this._handlers,
                interceptor => interceptor.InterceptClientJoined(evt),
                subscriber => subscriber.OnClientJoined(this, connection));
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientLeft(IServerConnection connection)
        {
            logger.Info("Client {0} disconnected.", connection.Address);

            var evt = new ClientLeftEvent(this, connection);

            this.HandleEvent(
                evt, this._handlers,
                interceptor => interceptor.InterceptClientLeft(evt),
                subscriber => subscriber.OnClientLeft(this, connection));
        }
        /// <summary>
        /// Accepts the connection.
        /// </summary>
        public virtual IServerConnection AcceptConnection()
        {
            return this.Protocol.AcceptConnection();
        }
        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <param name="package">The package.</param>
        private IEnumerable<IServerHandler> GetSubscribers(Package package)
        {
            return this._handlers.Where(s => s.PackageType.IsInstanceOfType(package));
        }
        #endregion
        
        #region Implementation of IServer
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public IList<IServerConnection> Connections
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
            var cachedList = (CachedList<IServerConnection>)this.Connections;

            using (cachedList.StartCaching())
            {
                foreach (IServerConnection connection in this.Connections)
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
        public void Send(Package package, IServerConnection receiver)
        {
            logger.Trace("Sending {0} to {1}.", package.GetType().Name, receiver.Address);

            if (this.OnBeginSendPackage(package, receiver))
            {
                this._connectionManager.GetOutputQueue(receiver).Enqueue(package);
                this.OnSentPackage(package, receiver);
            }
        }
        /// <summary>
        /// Subscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IServerHandler subscriber)
        {
            this._handlers.Add(subscriber);
        }
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IServerInterceptor subscriber)
        {
            this._interceptors.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IServerHandler subscriber)
        {
            this._handlers.Remove(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IServerInterceptor subscriber)
        {
            this._interceptors.Add(subscriber);
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
