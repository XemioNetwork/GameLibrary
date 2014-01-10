using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Network.Internal.Dispatchers;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class ServerConnectionManager : Worker
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly Dictionary<ISender, ServerPackageDispatcher> _dispatcherMappings;
        private readonly Dictionary<ISender, OutputQueue> _queueMappings; 
        #endregion Fields

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
            this._dispatcherMappings = new Dictionary<ISender, ServerPackageDispatcher>();
            this._queueMappings = new Dictionary<ISender, OutputQueue>();

            this.Server = server;
            this.Server.Subscribe(new ServerDisconnectHandler(this));

            this.Connections = new CachedList<IServerConnection>();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates a new output queue for the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void CreateOutputQueue(IServerConnection connection)
        {
            if (!this._queueMappings.ContainsKey(connection))
            {
                logger.Debug("Creating output queue for connection {0}.", connection.Address);

                var outputQueue = new OutputQueue(connection);
                outputQueue.Start();

                this._queueMappings.Add(connection, outputQueue);
            }
        }
        /// <summary>
        /// Creates a new connection dispatcher to handle incoming packages from a client.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void CreateDispatcher(IServerConnection connection)
        {
            if (!this._dispatcherMappings.ContainsKey(connection))
            {
                logger.Debug("Creating dispatcher for connection {0}.", connection.Address);

                var dispatcher = new ServerPackageDispatcher(this, connection);
                dispatcher.Start();

                this._dispatcherMappings.Add(connection, dispatcher);
            }
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
                this.CreateDispatcher(connection);
                this.CreateOutputQueue(connection);

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
                this._dispatcherMappings.Remove(connection);
                this._queueMappings.Remove(connection);

                this.Connections.Remove(connection);
            }

            logger.Debug("Removed {0} from connection list.", connection.Address);
        }
        /// <summary>
        /// Gets the output queue for the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public OutputQueue GetOutputQueue(IServerConnection connection)
        {
            return this._queueMappings[connection];
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
