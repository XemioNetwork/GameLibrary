using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Servers
{
    public class ServerSendingPackageEvent : ServerPackageEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSendingPackageEvent" /> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="channel">The Channel.</param>
        public ServerSendingPackageEvent(Package package, ServerChannel channel) : base(package, channel)
        {
        }
        #endregion
    }
}
