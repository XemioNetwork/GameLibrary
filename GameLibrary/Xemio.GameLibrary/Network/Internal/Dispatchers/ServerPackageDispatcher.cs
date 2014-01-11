using System;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Threads;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal.Dispatchers
{
    internal class ServerPackageDispatcher : Worker
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly IServer _server;
        private readonly ServerConnectionManager _connectionManager;

        private readonly IServerConnection _connection;
        private readonly IThreadInvoker _threadInvoker;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerPackageDispatcher" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connectionManager">The connection manager.</param>
        /// <param name="connection">The connection.</param>
        public ServerPackageDispatcher(ServerConnectionManager connectionManager, IServerConnection connection)
        {
            this._connectionManager = connectionManager;
            this._server = connectionManager.Server;

            this._connection = connection;
            this._threadInvoker = XGL.Components.Get<IThreadInvoker>();
        }
        #endregion

        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            try
            {
                while (this._connection.Connected)
                {
                    Package package = this._connection.Receive();
                    if (package != null)
                    {
                        logger.Trace("Received {0} from {1}.", package.GetType().Name, this._connection.Address);
                        this._threadInvoker.Invoke(() => this._server.OnReceivePackage(package, this._connection));
                    }
                }
            }
            catch (ConnectionClosedException ex) { }
            catch (Exception ex)
            {
                logger.ErrorException(string.Format("Error while receiving package from {0}.", this._connection.Address), ex);
            }
            finally
            {
                this._server.OnClientLeft(this._connection);
            }
        }
        #endregion
    }
}
