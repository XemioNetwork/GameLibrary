using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Synchronization
{
    public interface ISynchronizable
    {
        /// <summary>
        /// Synchronizes to the specified storage.
        /// </summary>
        /// <param name="storage">The storage.</param>
        void Synchronize(SynchronizationStorage storage);
    }
}
