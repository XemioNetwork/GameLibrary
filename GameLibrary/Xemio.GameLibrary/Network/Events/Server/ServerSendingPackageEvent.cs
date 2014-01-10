using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Server
{
    public class ServerSendingPackageEvent : ServerPackageEvent, IInterceptableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSendingPackageEvent" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public ServerSendingPackageEvent(IServer server, Package package, IServerConnection connection) : base(server, package, connection)
        {
        }
        #endregion

        #region Implementation of IInterceptableEvent
        /// <summary>
        /// Gets a value indicating whether the event propagation was canceled.
        /// </summary>
        public bool IsCanceled { get; private set; }
        /// <summary>
        /// Cancels the event propagation.
        /// </summary>
        public void Cancel()
        {
            this.IsCanceled = true;
        }
        #endregion
    }
}
