using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Network.Handlers;

namespace Xemio.GameLibrary.Network.Timing
{
    public class LatencyClientHandler : ClientHandler<LatencyPackage>
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        /// <summary>
        /// Called when the client receives a latency package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(Client client, LatencyPackage package)
        {
            logger.Debug("Received latency information: {0}ms", package.Latency);
            client.Store(new LatencyInformation(package.Latency));
        }
        #endregion
    }
}
