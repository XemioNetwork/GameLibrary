using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class QueuePackage
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QueuePackage"/> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="time">The time.</param>
        public QueuePackage(Package package, float time)
        {
            this.Package = package;
            this.Time = time;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the package.
        /// </summary>
        public Package Package { get; private set; }
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public float Time { get; set; }
        #endregion
    }
}
