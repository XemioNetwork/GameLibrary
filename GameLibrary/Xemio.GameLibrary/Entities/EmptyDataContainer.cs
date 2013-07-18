using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Entities
{
    public class EmptyDataContainer : EntityDataContainer
    {
        #region Overrides of EntityDataContainer
        /// <summary>
        /// Synchronizes to the specified storage.
        /// </summary>
        /// <param name="storage">The storage.</param>
        public override void Synchronize(SynchronizationStorage storage)
        {
        }
        #endregion
    }
}
