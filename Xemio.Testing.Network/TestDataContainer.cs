using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.Testing.Network
{
    public class TestDataContainer : EntityDataContainer
    {
        #region Properties
        /// <summary>
        /// Gets or sets the Velocity.
        /// </summary>
        public float Velocity { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Synchronizes to the specified storage.
        /// </summary>
        /// <param name="storage">The storage.</param>
        public override void Synchronize(SynchronizationStorage storage)
        {
            storage.Register(() => this.Velocity);
        }
        #endregion
    }
}
