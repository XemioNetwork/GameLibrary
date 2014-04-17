using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Dispatchers;
using Xemio.GameLibrary.Network.Events.Clients;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Intercetors;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Game;

namespace Xemio.GameLibrary.Network
{
    public class Client : IDisposable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public Client(string url)
        {
            logger.Info("Creating client for [{0}].", url);

            this.Protocol = ProtocolFactory.CreateClientProtocol(url);
            this.Protocol.Client = this;

            this._storage = new ObjectStorage();

            this._handlers = new List<IClientHandler>();
            this._interceptors = new List<IClientInterceptor>();

            //Setup events before the dispatcher and output queue get initialized
            this.SetupEvents();

            this._queue = new ClientOutputQueue(this);
            this._dispatcher = new ClientPackageDispatcher(this);

            this.Subscribe(new LatencyClientHandler());
            this.Subscribe(new TimeSyncClientHandler());
        }
        #endregion

        #region Fields
        private readonly ObjectStorage _storage; 

        private readonly List<IClientHandler> _handlers;
        private readonly List<IClientInterceptor> _interceptors;
 
        private readonly OutputQueue _queue;
        private readonly ClientPackageDispatcher _dispatcher;

        private IDisposable _eventDisposable;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        internal IClientProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the sender is connected.
        /// </summary>
        public bool Connected
        {
            get { return this.Protocol.Connected; }
        }
        #endregion

        #region Implementation of ISender
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            var eventManager = XGL.Components.Require<IEventManager>();

            var sendingEvent = new ClientSendingPackageEvent(this, package);
            eventManager.Publish(sendingEvent);

            if (!sendingEvent.IsCanceled)
            {
                this._queue.Enqueue(package);
            }

            var sentEvent = new ClientSentPackageEvent(this, package);
            eventManager.Publish(sentEvent);
        }
        /// <summary>
        /// Subscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IClientHandler subscriber)
        {
            this._handlers.Add(subscriber);
        }
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IClientInterceptor subscriber)
        {
            this._interceptors.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IClientHandler subscriber)
        {
            this._handlers.Remove(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IClientInterceptor subscriber)
        {
            this._interceptors.Add(subscriber);
        }
        /// <summary>
        /// Stops the client.
        /// </summary>
        public void Close()
        {
            this._dispatcher.Interrupt();
            this.Protocol.Close();
        }
        #endregion

        #region Private Event Methods
        /// <summary>
        /// Intercepts the client disconnected event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void InterceptClientDisconnected(ClientLostConnectionEvent evt)
        {
            if (evt.Client == this)
            {
                this.Intercept(i => i.InterceptDisconnect(evt));
            }
        }
        /// <summary>
        /// Intercepts the client received package event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void InterceptClientReceivedPackage(ClientReceivedPackageEvent evt)
        {
            if (evt.Client == this)
            {
                this.Intercept(i => i.InterceptReceived(evt));
            }
        }
        /// <summary>
        /// Intercepts the client sending package event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void InterceptClientSendingPackage(ClientSendingPackageEvent evt)
        {
            if (evt.Client == this)
            {
                this.Intercept(i => i.InterceptSending(evt));
            }
        }
        /// <summary>
        /// Intercepts the client sent package event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void InterceptClientSentPackage(ClientSentPackageEvent evt)
        {
            if (evt.Client == this)
            {
                this.Intercept(i => i.InterceptSent(evt));
            }
        }
        /// <summary>
        /// Handles the client disconnected event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void HandleClientLostConnection(ClientLostConnectionEvent evt)
        {
            if (evt.Client == this)
            {
                this.Handle(this._handlers, handler => handler.OnLostConnection(this));
            }
        }
        /// <summary>
        /// Handles the client received package event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void HandleClientReceivedPackage(ClientReceivedPackageEvent evt)
        {
            if (evt.Client == this)
            {
                this.Handle(this.FindSubscribers(evt.Package), handler => handler.OnReceive(this, evt.Package));
            }
        }
        /// <summary>
        /// Handles the client sending package event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void HandleClientSendingPackage(ClientSendingPackageEvent evt)
        {
            if (evt.Client == this)
            {
                this.Handle(this.FindSubscribers(evt.Package), handler => handler.OnSending(this, evt.Package));
            }
        }
        /// <summary>
        /// Handles the client sent package event.
        /// </summary>
        /// <param name="evt">The event.</param>
        private void HandleClientSentPackage(ClientSentPackageEvent evt)
        {
            if (evt.Client == this)
            {
                this.Handle(this.FindSubscribers(evt.Package), handler => handler.OnSent(this, evt.Package));
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets up all needed events.
        /// </summary>
        private void SetupEvents()
        {
            var eventManager = XGL.Components.Require<IEventManager>();

            this._eventDisposable = Disposable.Combine(
                eventManager.Subscribe<ClientLostConnectionEvent>(this.InterceptClientDisconnected),
                eventManager.Subscribe<ClientReceivedPackageEvent>(this.InterceptClientReceivedPackage),
                eventManager.Subscribe<ClientSendingPackageEvent>(this.InterceptClientSendingPackage),
                eventManager.Subscribe<ClientSentPackageEvent>(this.InterceptClientSentPackage),
                eventManager.Subscribe<ClientLostConnectionEvent>(this.HandleClientLostConnection),
                eventManager.Subscribe<ClientReceivedPackageEvent>(this.HandleClientReceivedPackage),
                eventManager.Subscribe<ClientSendingPackageEvent>(this.HandleClientSendingPackage),
                eventManager.Subscribe<ClientSentPackageEvent>(this.HandleClientSentPackage));
        }
        /// <summary>
        /// Intercepts the specified event.
        /// </summary>
        /// <param name="intercept">The intercept method.</param>
        private void Intercept(Action<IClientInterceptor> intercept)
        {
            foreach (IClientInterceptor interceptor in this._interceptors)
            {
                intercept(interceptor);
            }
        }
        /// <summary>
        /// Processes the specified event.
        /// </summary>
        /// <param name="handlers">The handlers.</param>
        /// <param name="handle">The handler action.</param>
        private void Handle(IEnumerable<IClientHandler> handlers, Action<IClientHandler> handle)
        {
            foreach (IClientHandler handler in handlers)
            {
                handle(handler);
            }
        }
        /// <summary>
        /// Gets the subscribers for the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        private IEnumerable<IClientHandler> FindSubscribers(Package package)
        {
            return this._handlers.Where(s => s.PackageType.IsInstanceOfType(package));
        }
        #endregion
        
        #region Implementation of IStorage
        /// <summary>
        /// Stores the specified value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        public void Store<T>(T value)
        {
            this._storage.Store(typeof(T).AssemblyQualifiedName, value);
        }
        /// <summary>
        /// Retrieves a value of the specified type.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        public T Retrieve<T>()
        {
            return this._storage.Retrieve<T>(typeof(T).AssemblyQualifiedName);
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this._eventDisposable.Dispose();
        }
        #endregion
    }
}
