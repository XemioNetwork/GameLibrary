using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Server
{
    public class ServerReceivedPackageEvent : ServerPackageEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerReceivedPackageEvent" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public ServerReceivedPackageEvent(IServer server, Package package, IServerConnection connection) : base(server, package, connection)
        {
        }
        #endregion
    }
}
