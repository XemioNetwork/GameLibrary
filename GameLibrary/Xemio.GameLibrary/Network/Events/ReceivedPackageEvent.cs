using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events
{
    public class ReceivedPackageEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedPackageEvent"/> class.
        /// </summary>
        public ReceivedPackageEvent(Package package)
        {
            this.Package = package;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedPackageEvent"/> class.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public ReceivedPackageEvent(Package package, IConnection connection) : this(package)
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

        #region Methods
        /// <summary>
        /// Converts the package to the specified type.
        /// </summary>
        public T As<T>() where T : Package
        {
            return this.Package as T;
        }
        #endregion

        #region IEvent Member
        /// <summary>
        /// Gets a value indicating whether this <see cref="IEvent"/> is synced.
        /// </summary>
        public bool Synced
        {
            get { return false; }
        }
        #endregion
    }
}
