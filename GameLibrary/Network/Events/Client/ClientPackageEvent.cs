using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Client
{
    public abstract class ClientPackageEvent : ICancelableEvent
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
