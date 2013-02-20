using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events
{
    public abstract class SyncedEvent : Event
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncedEvent"/> class.
        /// </summary>
        public SyncedEvent()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="SyncedEvent"/> is sync.
        /// </summary>
        public virtual bool Sync
        {
            get { return true; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new package instance for the specified event.
        /// </summary>
        public abstract Package CreatePackage();
        #endregion
    }
}
