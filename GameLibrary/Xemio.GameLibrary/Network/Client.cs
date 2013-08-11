using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Logic;
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
    public class Client : IClient, IGameHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IClientProtocol protocol)
        {
            this._subscribers = new List<IClientLogic>();
            this._sender = new PackageSender();
            this._receiver = new ClientPackageReceiver(this);

            this.Protocol = protocol;
            this.Protocol.Client = this;

            this.Subscribe(new LatencyClientLogic());
            this.Subscribe(new TimeSyncClientLogic());

            this.Active = true;

            this._receiver.StartReceivingPackages();

            GameLoop loop = XGL.Components.Get<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Fields
        private readonly List<IClientLogic> _subscribers;
        private readonly PackageSender _sender;
        private readonly ClientPackageReceiver _receiver;
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
        private IEnumerable<IClientLogic> GetSubscribers(Package package)
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
            IEnumerable<IClientLogic> subscribers = this.GetSubscribers(package);
            foreach (IClientLogic subscriber in subscribers)
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
            IEnumerable<IClientLogic> subscribers = this.GetSubscribers(package);
            foreach (IClientLogic subscriber in subscribers)
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
            IEnumerable<IClientLogic> subscribers = this.GetSubscribers(package);
            foreach (IClientLogic subscriber in subscribers)
            {
                subscriber.OnSent(this, package);
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
            foreach (IClientLogic subscriber in this._subscribers)
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

            this._sender.Send(package, this.Protocol);

            this.OnSentPackage(package);
        }
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IClientLogic subscriber)
        {
            this._subscribers.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IClientLogic subscriber)
        {
            this._subscribers.Remove(subscriber);
        }
        #endregion
    }
}
