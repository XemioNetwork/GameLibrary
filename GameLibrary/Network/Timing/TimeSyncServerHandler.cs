using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Network.Handlers;

namespace Xemio.GameLibrary.Network.Timing
{
    public class TimeSyncServerHandler : ServerHandler<TimeSyncPackage>
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSyncServerHandler" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public TimeSyncServerHandler(Server server)
        {
            this._worker = new TimeSyncServerWorker(server, 2000);
        }
        #endregion

        #region Fields
        private readonly TimeSyncServerWorker _worker;
        #endregion
        
        #region Methods
        /// <summary>
        /// Called when the server is sending a time sync package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        public override void OnSending(ServerChannel receiver, TimeSyncPackage package)
        {
            receiver.Store(new TimeSyncInformation());
        }
        /// <summary>
        /// Called when the server receives a time sync package.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(ServerChannel sender, TimeSyncPackage package)
        {
            float latency = 0.0f;

            var information = sender.Retrieve<TimeSyncInformation>();
            if (information != null)
            {
                latency = information.RoundstripTime;
            }

            logger.Trace("Setting latency for {0} to {1}ms.", sender.Address, latency);

            sender.Store(new LatencyInformation(latency));
            sender.Send(new LatencyPackage { Latency = latency });
        }
        #endregion
    }
}
