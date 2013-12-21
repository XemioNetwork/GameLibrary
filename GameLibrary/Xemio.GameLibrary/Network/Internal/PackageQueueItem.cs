using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class PackageQueueItem
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageQueueItem"/> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public PackageQueueItem(Package package, ISender receiver)
        {
            this.Package = package;
            this.Receiver = receiver;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the package.
        /// </summary>
        public Package Package { get; private set; }
        /// <summary>
        /// Gets the receiver.
        /// </summary>
        public ISender Receiver { get; private set; }
        #endregion
    }
}