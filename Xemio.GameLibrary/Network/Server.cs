using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Network.Subscribers;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Game;

namespace Xemio.GameLibrary.Network
{
    public abstract class Server : IComponent, IGameHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        protected Server(IServerProtocol protocol)
        {
            GameLoop loop = XGL.GetComponent<GameLoop>();
            loop.Subscribe(this);
            
            this.Active = true;

            this.Protocol = protocol;
            this.Protocol.Server = this;

            this.PackageManager = new PackageManager(this.PackageAssembly);
            this.Connections = new List<IConnection>();

            this._eventManager = XGL.GetComponent<EventManager>();
            this._subscribers = new List<IServerSubscriber>();

            this.Subscribe(new TimeSyncServerSubscriber());
            this.StartServerLoop();
        }
        #endregion

        #region Fields
        private EventManager _eventManager;
        private List<IServerSubscriber> _subscribers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Server"/> is active.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the package manager.
        /// </summary>
        public PackageManager PackageManager { get; private set; }
        /// <summary>
        /// Gets the package assembly.
        /// </summary>
        public abstract Assembly PackageAssembly { get; }
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public List<IConnection> Connections { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <param name="package">The package.</param>
        private IEnumerable<IServerSubscriber> GetSubscribers(Package package)
        {
            return this._subscribers.Where(s => s.Type.IsAssignableFrom(package.GetType()));
        }
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IServerSubscriber subscriber)
        {
            this._subscribers.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Unsubscribe(IServerSubscriber subscriber)
        {
            this._subscribers.Remove(subscriber);
        }
        /// <summary>
        /// Provides the component.
        /// </summary>
        public void ProvideComponent()
        {
            XGL.Components.Add(new ValueProvider<Server>(this));
        }
        /// <summary>
        /// Called when the server received a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        protected virtual void OnReceivedPackage(Package package, IConnection connection)
        {
            IEnumerable<IServerSubscriber> subscribers = this.GetSubscribers(package);
            foreach (IServerSubscriber subscriber in subscribers)
            {
                subscriber.OnReceive(this, package, connection);    
            }

            this._eventManager.Send(new ReceivedPackageEvent(package, connection));
        }
        /// <summary>
        /// Called when server is sending a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        protected virtual void OnBeginSendPackage(Package package, IConnection connection)
        {
            IEnumerable<IServerSubscriber> subscribers = this.GetSubscribers(package);
            foreach (IServerSubscriber subscriber in subscribers)
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
            IEnumerable<IServerSubscriber> subscribers = this.GetSubscribers(package);
            foreach (IServerSubscriber subscriber in subscribers)
            {
                subscriber.OnSent(this, package, connection);
            }

            this._eventManager.Send(new SentPackageEvent(package, connection));
        }
        /// <summary>
        /// Starts the server loop.
        /// </summary>
        private void StartServerLoop()
        {
            Action serverLoop = this.ServerLoop;
            Task loopTask = new Task(serverLoop);

            loopTask.Start();
        }
        /// <summary>
        /// Accepts incoming client connection requests.
        /// </summary>
        private void ServerLoop()
        {
            while (this.Active)
            {
                IConnection connection = this.Protocol.AcceptConnection();
                this._eventManager.Send(new ClientJoinedEvent(connection));

                this.Connections.Add(connection);
                this.StartListening(connection);
            }
        }
        /// <summary>
        /// Starts listening to the connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void StartListening(IConnection connection)
        {
            Task listenTask = new Task(() => this.Listen(connection));
            listenTask.Start();
        }
        /// <summary>
        /// Listens to the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void Listen(IConnection connection)
        {
            while (connection.Connected)
            {
                try
                {
                    Package package = connection.Receive();
                    this.OnReceivedPackage(package, connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.StackTrace);
                    //TODO: logging
                    this.Connections.Remove(connection);
                    this._eventManager.Send(new ClientLeftEvent(connection));
                    break;
                }
            }
        }
        /// <summary>
        /// Sends the specified package to all clients.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            foreach (IConnection connection in this.Connections)
            {
                this.Send(package, connection);
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

            this.Protocol.Send(package, receiver);
            this.OnSentPackage(package, receiver);
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            foreach (IServerSubscriber subscriber in this._subscribers)
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
