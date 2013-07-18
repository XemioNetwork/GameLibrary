using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Entities
{
    public abstract class EntityDataContainer : ISynchronizable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDataContainer"/> class.
        /// </summary>
        protected EntityDataContainer()
        {
            this.Storage = new SynchronizationStorage(this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the storage.
        /// </summary>
        [ExcludeSync]
        public SynchronizationStorage Storage { get; private set; }
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the empty instance.
        /// </summary>
        [ExcludeSync]
        public static EntityDataContainer Empty
        {
            get { return Singleton<EmptyDataContainer>.Value; }
        }
        #endregion

        #region ISynchronizable Member
        /// <summary>
        /// Gets the ID.
        /// </summary>
        [ExcludeSync]
        public int Id
        {
            get { return this.GetType().FullName.GetHashCode(); }
        }
        /// <summary>
        /// Synchronizes to the specified storage.
        /// </summary>
        /// <param name="storage">The storage.</param>
        public abstract void Synchronize(SynchronizationStorage storage);
        #endregion
    }
}
