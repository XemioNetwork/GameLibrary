using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Handlers;

namespace Xemio.GameLibrary.Network.Timing
{
    public class TimeSyncClientHandler : ClientHandler<TimeSyncPackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(IClient client, TimeSyncPackage package)
        {
            client.Send(package);
        }
        #endregion
    }
}
