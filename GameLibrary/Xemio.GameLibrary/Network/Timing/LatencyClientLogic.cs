using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Logic;

namespace Xemio.GameLibrary.Network.Timing
{
    public class LatencyClientLogic : ClientLogic<LatencyPackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a latency package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(IClient client, LatencyPackage package)
        {
            client.Latency = package.Latency;
        }
        #endregion
    }
}
