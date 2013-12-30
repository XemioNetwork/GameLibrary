using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Internal;
using Xemio.GameLibrary.Network.Packages.Dispatchers;
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
    public class Client : IClient, IComponent
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

            this._subscribers = new List<IClientHandler>();

            this._outputQueue = new OutputQueue(this.Protocol);
            this._outputQueue.Start();

            this.Subscribe(new LatencyClientHandler());
            this.Subscribe(new TimeSyncClientHandler());
            
            this._dispatcher = new ClientPackageDispatcher(this);
            this._dispatcher.Start();

            this._eventManager = XGL.Components.Require<EventManager>();
        }
        #endregion

        #region Fields
        private readonly List<IClientHandler> _subscribers;
        private readonly OutputQueue _outputQueue;
        private readonly ClientPackageDispatcher _dispatcher;

        private readonly EventManager _eventManager;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        public IClientProtocol Protocol { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <param name="package">The package.</param>
        private IEnumerable<IClientHandler> GetSubscribers(Package package)
        {
            return this._subscribers
                .Where(s => s.PackageType.IsInstanceOfType(package));
        }
        /// <summary>
        /// Called when the client gets disconnected.
        /// </summary>
        public virtual void OnDisconnected()
        {
            foreach (IClientHandler subscriber in this._subscribers)
            {
                subscriber.OnDisconnected(this);
            }
        }
        /// <summary>
        /// Calls the IClientLogics when the specified package was received.
        /// </summary>
        /// <param name="package">The package.</param>
        public virtual void OnReceivePackage(Package package)
        {
            IEnumerable<IClientHandler> subscribers = this.GetSubscribers(package);
            foreach (IClientHandler subscriber in subscribers)
            {
                subscriber.OnReceive(this, package);
            }

            this._eventManager.Publish(new ReceivedPackageEvent(package));
        }
        /// <summary>
        /// Calls the IClientLogics when the specified package is going to be send.
        /// </summary>
        /// <param name="package">The package.</param>
        public virtual void OnBeginSendPackage(Package package)
        {
            IEnumerable<IClientHandler> subscribers = this.GetSubscribers(package);
            foreach (IClientHandler subscriber in subscribers)
            {
                subscriber.OnBeginSend(this, package);
            }
        }
        /// <summary>
        /// Calls the IClientLogics when the specified package is sent.
        /// </summary>
        /// <param name="package">The package.</param>
        public virtual void OnSentPackage(Package package)
        {
            IEnumerable<IClientHandler> subscribers = this.GetSubscribers(package);
            foreach (IClientHandler subscriber in subscribers)
            {
                subscriber.OnSent(this, package);
            }

            this._eventManager.Publish(new SentPackageEvent(package));
        }
        #endregion

        #region Implementation of IClient
        /// <summary>
        /// Gets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Gets a value indicating whether the sender is connected.
        /// </summary>
        public bool Connected
        {
            get { return this.Protocol.Connected; }
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            this.OnBeginSendPackage(package);
            this._outputQueue.Enqueue(package);
            this.OnSentPackage(package);
        }
        /// <summary>
        /// Subscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IClientHandler subscriber)
        {
            this._subscribers.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified package handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IClientHandler subscriber)
        {
            this._subscribers.Remove(subscriber);
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
    }
}
