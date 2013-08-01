using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Logic;

namespace Xemio.GameLibrary.Network.Timing
{
    public class TimeSyncClientLogic : ClientLogic<TimeSyncPackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(Client client, TimeSyncPackage package)
        {
            client.Send(package);
        }
        #endregion
    }
}
