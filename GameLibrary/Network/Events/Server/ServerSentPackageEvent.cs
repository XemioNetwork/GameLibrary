using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Server
{
    public class ServerSentPackageEvent : ServerPackageEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSentPackageEvent" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public ServerSentPackageEvent(IServer server, Package package, IServerConnection connection) : base(server, package, connection)
        {
        }
        #endregion
    }
}
