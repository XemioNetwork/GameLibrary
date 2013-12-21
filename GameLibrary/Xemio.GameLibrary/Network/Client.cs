using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Internal;
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
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public Client(string url)
        {
            this._subscribers = new List<IClientHandler>();
            this._queue = new PackageQueue();

            this.Protocol = ProtocolFactory.CreateClientProtocol(url);
            this.Protocol.Client = this;

            this.Subscribe(new LatencyClientHandler());
            this.Subscribe(new TimeSyncClientHandler());

            this.Active = true;

            this._processor = new ClientPackageProcessor(this);
            this._processor.Start();
        }
        #endregion

        #region Fields
        private readonly List<IClientHandler> _subscribers;
        private readonly PackageQueue _queue;
        private readonly ClientPackageProcessor _processor;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Client"/> is active.
        /// </summary>
        public bool Active { get; set; }
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
        /// Calls the IClientLogics when the specified package was received.
        /// </summary>
        /// <param name="package">The package.</param>
        protected internal virtual void OnReceivePackage(Package package)
        {
            IEnumerable<IClientHandler> subscribers = this.GetSubscribers(package);
            foreach (IClientHandler subscriber in subscribers)
            {
                subscriber.OnReceive(this, package);
            }
        }
        /// <summary>
        /// Calls the IClientLogics when the specified package is going to be send.
        /// </summary>
        /// <param name="package">The package.</param>
        protected virtual void OnBeginSendPackage(Package package)
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
        protected virtual void OnSentPackage(Package package)
        {
            IEnumerable<IClientHandler> subscribers = this.GetSubscribers(package);
            foreach (IClientHandler subscriber in subscribers)
            {
                subscriber.OnSent(this, package);
            }
        }
        #endregion

        #region Implementation of IClient
        /// <summary>
        /// Gets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        public IClientProtocol Protocol { get; private set; }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            if (!this.Protocol.Connected)
                throw new InvalidOperationException("You have to connect to a server first.");

            this.OnBeginSendPackage(package);
            this._queue.Offer(package, this.Protocol);
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
            this._processor.Interrupt();
            this.Protocol.Close();
        }
        #endregion
    }
}
