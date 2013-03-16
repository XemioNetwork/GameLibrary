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
        /// Creates a new world update for the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        void Create(EntityEnvironment environment);
    }
}
