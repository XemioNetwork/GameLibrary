using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Clients
{
    public class ClientSentPackageEvent : ClientPackageEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPackageEvent"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public ClientSentPackageEvent(Client client, Package package) : base(client, package)
        {
        }
        #endregion
    }
}
