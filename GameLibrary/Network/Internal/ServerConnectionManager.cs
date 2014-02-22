using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Common.Threads;
using Xemio.GameLibrary.Network.Internal.Dispatchers;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class ServerConnectionManager : Worker
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly Dictionary<IServerConnection, InternalConnection> _mappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the server.
        /// </summary>
        public IServer Server { get; private set; }
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public IList<IServerConnection> Connections { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionManager"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public ServerConnectionManager(IServer server)
        {
            this._mappings = new Dictionary<IServerConnection, InternalConnection>();

            this.Server = server;
            this.Server.Subscribe(new ServerDisconnectHandler(this));

            this.Connections = new AutoCachedList<IServerConnection>();
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void Add(IServerConnection connection)
        {
            lock (this.Connections)
            {
                this._mappings.Add(connection, new InternalConnection(this, connection));
                this.Connections.Add(connection);
            }

            logger.Debug("Added {0} to connection list.", connection.Address);
        }
        /// <summary>
        /// Removes the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void Remove(IServerConnection connection)
        {
            lock (this.Connections)
            {
                this._mappings.Remove(connection);
                this.Connections.Remove(connection);
            }

            logger.Debug("Removed {0} from connection list.", connection.Address);
        }
        /// <summary>
        /// Enqueues the specified package to the receivers output queue.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        public void Enqueue(IServerConnection receiver, Package package)
        {
            this._mappings[receiver].Queue.Enqueue(package);
        }
        #endregion

        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            while (this.IsRunning() && this.Server.Connected)
            {
                IServerConnection connection = this.Server.AcceptConnection();

                this.Add(connection);
                if (!this.Server.OnClientJoined(connection))
                {
                    connection.Disconnect();
                }
            }
        }
        #endregion
    }
}
