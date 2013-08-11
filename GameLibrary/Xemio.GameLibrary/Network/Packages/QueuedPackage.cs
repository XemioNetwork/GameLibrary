using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Packages
{
    internal class QueuedPackage
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QueuedPackage"/> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public QueuedPackage(Package package, IPackageSender receiver)
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
        public IPackageSender Receiver { get; private set; }
        #endregion
    }
}
