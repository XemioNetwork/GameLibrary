using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class ServerDisconnectHandler : ServerHandler<Package>
    {
        #region Fields
        private readonly ServerConnectionManager _connectionManager;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerDisconnectHandler"/> class.
        /// </summary>
        /// <param name="connectionManager">The connectionManager.</param>
        public ServerDisconnectHandler(ServerConnectionManager connectionManager)
        {
            this._connectionManager = connectionManager;
        }
        #endregion

        #region Overrides of ServerHandler<Package>
        /// <summary>
        /// Called when a client left.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public override void OnClientLeft(IServer server, IServerConnection connection)
        {
            this._connectionManager.Remove(connection);
        }
        #endregion
    }
}
