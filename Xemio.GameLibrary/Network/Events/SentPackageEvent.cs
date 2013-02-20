using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events
{
    public class SentPackageEvent : Event
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SentPackageEvent"/> class.
        /// </summary>
        public SentPackageEvent(Package package)
        {
            this.Package = package;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SentPackageEvent"/> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public SentPackageEvent(Package package, IConnection connection) : this(package)
        {
            this.Connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the package.
        /// </summary>
        public Package Package { get; private set; }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IConnection Connection { get; private set; }
        #endregion
    }
}
