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

            this.Protocol = protocol;
            this.Protocol.Client = this;

            this.Subscribe(new LatencyClientLogic());
            this.Subscribe(new TimeSyncClientLogic());

            this.Active = true;
            this.StartLoop();

            GameLoop loop = XGL.Components.Get<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Fields
        private readonly List<IClientLogic> _subscribers;
        private readonly PackageSender _sender;
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
        private IList<IClientLogic> GetSubscribers(Package package)
        {
            return this._subscribers
                .Where(s => s.Type.IsInstanceOfType(package))
                .ToList();
        }
        /// <summary>
        /// Starts the client loop.
        /// </summary>
        private void StartLoop()
        {
            Task.Factory.StartNew(this.ClientLoop);
        }
        /// <summary>
        /// Awaits the connection.
        /// </summary>
        private void AwaitConnection()
        {
            while (!this.Protocol.Connected)
            {
            }
        }
        /// <summary>
        /// Listens to the specified protocol and receives packages.
        /// </summary>
        private void ClientLoop()
        {
            var eventManager = XGL.Components.Get<EventManager>();
            var invoker = XGL.Components.Get<ThreadInvoker>();

            this.AwaitConnection();

            try
            {
                while (this.Active && this.Protocol.Connected)
                {
                    Package package = this.Protocol.Receive();
                    if (package != null)
                    {
                        this.Receive(package);
                        eventManager.Publish(new ReceivedPackageEvent(package));
                    }
                }
            }
            catch (IOException ex)
            {
                eventManager.Publish(new ExceptionEvent(ex));
            }
        }
        /// <summary>
        /// Receives the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Receive(Package package)
        {
            if (!this.Protocol.Connected)
                throw new InvalidOperationException("You have to connect to a server first.");

            IEnumerable<IClientLogic> subscribers = this.GetSubscribers(package);
            foreach (IClientLogic subscriber in subscribers)
            {
                subscriber.OnReceive(this, package);
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
        public float Latency { get; internal set; }
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

            IEnumerable<IClientLogic> subscribers = this.GetSubscribers(package);
            foreach (IClientLogic subscriber in subscribers)
            {
                subscriber.OnBeginSend(this, package);
            }

            this._sender.Send(package, this.Protocol);

            EventManager eventManager = XGL.Components.Get<EventManager>();
            eventManager.Publish(new SentPackageEvent(package));

            foreach (IClientLogic subscriber in subscribers)
            {
                subscriber.OnSent(this, package);
            }
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
