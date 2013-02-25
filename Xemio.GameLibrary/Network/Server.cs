using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Protocols;
using System.Reflection;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Network
{
    public abstract class Server : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public Server(IServerProtocol protocol)
        {
            this.Active = true;

            this.Protocol = protocol;
            this.Protocol.Server = this;

            this.PackageManager = new PackageManager(this.PackageAssembly);
            this.Connections = new List<IConnection>();

            this._eventManager = XGL.GetComponent<EventManager>();
            this.StartServerLoop();
        }
        #endregion

        #region Fields
        private EventManager _eventManager;
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
        /// Called when the server received a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        private void OnReceivedPackage(Package package, IConnection connection)
        {
            this._eventManager.Send(new ReceivedPackageEvent(package, connection));
        }
        /// <summary>
        /// Called when server sent a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        private void OnSentPackage(Package package, IConnection connection)
        {
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

                EventManager eventManager = XGL.GetComponent<EventManager>();
                eventManager.Send(new ClientJoinedEvent(connection));

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
            Action action = () => this.Listen(connection);
            Task listenTask = new Task(action);

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
            this.OnSentPackage(package, receiver);
            this.Protocol.Send(package, receiver);
        }
        #endregion
    }
}
