using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Client
{
    public abstract class ClientPackageEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPackageEvent"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        protected ClientPackageEvent(IClient client, Package package)
        {
            this.Client = client;
            this.Package = package;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the client.
        /// </summary>
        public IClient Client { get; private set; }
        /// <summary>
        /// Gets the package.
        /// </summary>
        public Package Package { get; private set; }
        #endregion
    }
}
