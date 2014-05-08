using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Events.Servers;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Intercetors;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Game;

namespace Xemio.GameLibrary.Network
{
    public class Server : IDisposable
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

            this.Connections = new AutoProtectedList<ServerChannel>();
            this.MaxConnections = -1;

            this.Subscribe(new TimeSyncServerHandler(this));
            this.SetupEvents();

            Task.Factory.StartNew(
                this.AcceptClients, 
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }
        #endregion

        #region Fields
        private readonly List<IServerHandler> _handlers;
        private readonly List<IServerInterceptor> _interceptors;

        private IDisposable _eventDisposable;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        protected internal IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public IList<ServerChannel> Connections { get; private set; }
        /// <summary>
        /// Gets or sets the maximum connections. Set it to -1 to disable the limitation.
        /// </summary>
        public int MaxConnections { get; set; }
        /// <summary>
        /// Gets a value indicating whether the server is alive.
        /// </summary>
        public bool Connected { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the subscribers for the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        private IEnumerable<IServerHandler> FindSubscribers(Package package)
        {
            return this._handlers.Where(s => s.PackageType.IsInstanceOfType(package));
        }
        /// <summary>
        /// Accepts incoming connect requests.
        /// </summary>
        private void AcceptClients()
        {
            var eventManager = XGL.Components.Require<IEventManager>();

            while (this.Connected)
            {
                ServerChannel channel = this.AcceptChannel();
                
                var channelEvent = new ChannelOpenedEvent(channel);
                eventManager.Publish(channelEvent);

                if (this.Connections.Count == this.MaxConnections || channelEvent.IsCanceled)
                {
                    channel.Close();
                    continue;
                }

                lock (this.Connections)
                {
                    this.Connections.Add(channel);
                }
            }
        }
        #endregion

        #region Private Event Methods
        /// <summary>
        /// Sets up event handlers for server events.
        /// </summary>
        private void SetupEvents()
        {
            var eventManager = XGL.Components.Require<IEventManager>();

            this._eventDisposable = Disposable.Combine(
                eventManager.Subscribe<ServerReceivedPackageEvent>(this.InterceptServerReceivedPackage),
                eventManager.Subscribe<ServerSendingPackageEvent>(this.InterceptServerSendingPackage),
                eventManager.Subscribe<ServerSentPackageEvent>(this.InterceptServerSentPackage),
                eventManager.Subscribe<ChannelClosedEvent>(this.InterceptChannelClosedEvent),
                eventManager.Subscribe<ChannelOpenedEvent>(this.InterceptChannelOpened),

                eventManager.Subscribe<ServerReceivedPackageEvent>(this.HandleServerReceivedPackage),
                eventManager.Subscribe<ServerSendingPackageEvent>(this.HandleServerSendingPackage),
                eventManager.Subscribe<ServerSentPackageEvent>(this.HandleServerSentPackage),
                eventManager.Subscribe<ChannelClosedEvent>(this.HandleChannelClosed),
                eventManager.Subscribe<ChannelOpenedEvent>(this.HandleChannelOpened));
        }
        /// <summary>
        /// Intercepts the specified event.
        /// </summary>
        /// <param name="intercept">The intercept method.</param>
        private void Intercept(Action<IServerInterceptor> intercept)
        {
            foreach (IServerInterceptor interceptor in this._interceptors)
            {
                intercept(interceptor);
            }
        }
        /// <summary>
        /// Processes the specified event.
        /// </summary>
        /// <param name="handlers">The handlers.</param>
        /// <param name="handle">The handler action.</param>
        private void Handle(IEnumerable<IServerHandler> handlers, Action<IServerHandler> handle)
        {
            foreach (IServerHandler handler in handlers)
            {
                handle(handler);
            }
        }
        /// <summary>
        /// Intercepts the server received package.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void InterceptServerReceivedPackage(ServerReceivedPackageEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Intercept(interceptor => interceptor.InterceptReceived(evt));
            }
        }
        /// <summary>
        /// Intercepts the server sending package.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void InterceptServerSendingPackage(ServerSendingPackageEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Intercept(interceptor => interceptor.InterceptSending(evt));
            }
        }
        /// <summary>
        /// Intercepts the server sent package.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void InterceptServerSentPackage(ServerSentPackageEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Intercept(interceptor => interceptor.InterceptSent(evt));
            }
        }
        /// <summary>
        /// Intercepts the channel closed event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void InterceptChannelClosedEvent(ChannelClosedEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Intercept(interceptor => interceptor.InterceptChannelClosed(evt));
            }
        }
        /// <summary>
        /// Intercepts the channel opened.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void InterceptChannelOpened(ChannelOpenedEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Intercept(interceptor => interceptor.InterceptChannelOpened(evt));
            }
        }
        /// <summary>
        /// Handles the server received package.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void HandleServerReceivedPackage(ServerReceivedPackageEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Handle(this.FindSubscribers(evt.Package), handler => handler.OnReceive(evt.Channel, evt.Package));
            }
        }
        /// <summary>
        /// Handles the server sending package.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void HandleServerSendingPackage(ServerSendingPackageEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Handle(this.FindSubscribers(evt.Package), handler => handler.OnSending(evt.Channel, evt.Package));
            }
        }
        /// <summary>
        /// Handles the server sent package.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void HandleServerSentPackage(ServerSentPackageEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Handle(this.FindSubscribers(evt.Package), handler => handler.OnSent(evt.Channel, evt.Package));
            }
        }
        /// <summary>
        /// Handles the channel closed.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void HandleChannelClosed(ChannelClosedEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Handle(this._handlers, handler => handler.OnChannelClosed(evt.Channel));
            }
        }
        /// <summary>
        /// Handles the channel opened.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void HandleChannelOpened(ChannelOpenedEvent evt)
        {
            if (evt.Channel.Server == this)
            {
                this.Handle(this._handlers, handler => handler.OnChannelOpened(evt.Channel));
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Accepts a new server channel.
        /// </summary>
        protected virtual ServerChannel AcceptChannel()
        {
            return new ServerChannel(this, this.Protocol.AcceptChannel());
        }
        #endregion
        
        #region Implementation of ISender
        /// <summary>
        /// Sends the specified package to all clients.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            foreach (ServerChannel channel in this.Connections)
            {
                channel.Send(package);
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
            this.Protocol.Close();
            this.Connected = false;
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.Connected)
            {
                this.Close();
            }

            this._eventDisposable.Dispose();
        }
        #endregion
    }
}
