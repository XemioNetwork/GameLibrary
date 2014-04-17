using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Servers
{
    public abstract class ServerPackageEvent : ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerReceivedPackageEvent" /> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="channel">The Channel.</param>
        protected ServerPackageEvent(Package package, ServerChannel channel)
        {
            this.Package = package;
            this.Channel = channel;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the package.
        /// </summary>
        public Package Package { get; private set; }
        /// <summary>
        /// Gets the Channel.
        /// </summary>
        public ServerChannel Channel { get; private set; }
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

        #region Methods
        /// <summary>
        /// Converts the package to the specified type.
        /// </summary>
        public T As<T>() where T : Package
        {
            return this.Package as T;
        }
        #endregion
    }
}
