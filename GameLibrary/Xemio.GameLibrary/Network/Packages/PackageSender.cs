using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network.Packages
{
    public class PackageSender
    {
        #region Fields
        private readonly object _lock = new object();
        #endregion
        
        #region Methods
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        public void Send(Package package, IClientProtocol connection)
        {
            connection.Send(package);
        }
        #endregion
    }
}
