using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Network.Internal.Dispatchers;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class InternalConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalConnection" /> class.
        /// </summary>
        /// <param name="connectionManager">The connection manager.</param>
        /// <param name="connection">The connection.</param>
        public InternalConnection(ServerConnectionManager connectionManager, IServerConnection connection)
        {
            this.Queue = new OutputQueue(connection);
            this.Dispatcher = new ServerPackageDispatcher(connectionManager, connection);

            this.Queue.Start();
            this.Dispatcher.Start();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the queue.
        /// </summary>
        public OutputQueue Queue { get; private set; }
        /// <summary>
        /// Gets the dispatcher.
        /// </summary>
        public ServerPackageDispatcher Dispatcher { get; private set; }
        #endregion
    }
}
