using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Servers
{
    public class ServerReceivedPackageEvent : ServerPackageEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerReceivedPackageEvent" /> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="channel">The Channel.</param>
        public ServerReceivedPackageEvent(Package package, ServerChannel channel) : base(package, channel)
        {
        }
        #endregion
    }
}
