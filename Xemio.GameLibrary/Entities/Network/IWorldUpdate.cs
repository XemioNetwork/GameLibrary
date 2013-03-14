using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Network;

namespace Xemio.GameLibrary.Entities.Network
{
    public interface IWorldUpdate
    {
        /// <summary>
        /// Applies the specified snapshot.
        /// </summary>
        /// <param name="environment">The environment.</param>
        void Apply(EntityEnvironment environment);
    }
}
