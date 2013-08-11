using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network
{
    public interface IPackageSender
    {
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        void Send(Package package);
    }
}
