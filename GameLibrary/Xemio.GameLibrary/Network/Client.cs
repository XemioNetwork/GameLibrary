﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
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
    public class Client : IComponent, IGameHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IClientProtocol protocol)
        {
            this._subscribers = new List<IClientLogic>();

            this.Protocol = protocol;
            this.Protocol.Client = this;

            this.Subscribe(new LatencyClientLogic());
            this.Subscribe(new TimeSyncClientLogic());

            this.Active = true;
            this.Serializer = new PackageSerializer();

            this.StartLoop();

            GameLoop loop = XGL.Components.Get<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Fields
        private List<IClientLogic> _subscribers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Client"/> is active.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Gets the latency.
        /// </summary>
        public float Latency { get; internal set; }
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        public IClientProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the package manager.
        /// </summary>
        public PackageSerializer Serializer { get; private set; }
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
        /// <summary>
        /// Starts the client loop.
        /// </summary>
        private void StartLoop()
        {
            Task.Factory.StartNew(this.ClientLoop);
        }
        /// <summary>
        /// Listens to the specified protocol and receives packages.
        /// </summary>
        private void ClientLoop()
        {
            var invoker = XGL.Components.Get<ThreadInvoker>();

            while (this.Active)
            {
                Package package = this.Protocol.Receive();
                if (package != null)
                {
                    invoker.Invoke(() => this.Receive(package));

                    EventManager eventManager = XGL.Components.Get<EventManager>();
                    eventManager.Publish(new ReceivedPackageEvent(package));
                }
            }
        }
        /// <summary>
        /// Receives the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Receive(Package package)
        {
            IEnumerable<IClientLogic> subscribers = this.GetSubscribers(package);
            foreach (IClientLogic subscriber in subscribers)
            {
                subscriber.OnReceive(this, package);
            }
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            IEnumerable<IClientLogic> subscribers = this.GetSubscribers(package);
            foreach (IClientLogic subscriber in subscribers)
            {
                subscriber.OnBeginSend(this, package);
            }

            this.Protocol.Send(package);

            EventManager eventManager = XGL.Components.Get<EventManager>();
            eventManager.Publish(new SentPackageEvent(package));

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
                subscriber.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public void Render()
        {
        }
        #endregion
    }
}
