using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using NLog;
using Xemio.GameLibrary.Components.Attributes;
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
        public TimeSyncServerHandler(IServer server)
        {
            this._worker = new TimeSyncServerWorker(server, 2000);
            this._worker.Start();

            this._watches = new Dictionary<IConnection, Stopwatch>();
        }
        #endregion

        #region Fields
        private readonly TimeSyncServerWorker _worker;
        private readonly Dictionary<IConnection, Stopwatch> _watches;
        #endregion
        
        #region Methods
        /// <summary>
        /// Starts a new stop watch.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void StartWatch(IConnection connection)
        {
            if (!this._watches.ContainsKey(connection))
            {
                this._watches.Add(connection, new Stopwatch());
            }

            this._watches[connection] = Stopwatch.StartNew();
        }
        /// <summary>
        /// Gets the latency.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private float GetLatency(IConnection connection)
        {
            if (this._watches.ContainsKey(connection))
            {
                return (float)this._watches[connection].Elapsed.TotalMilliseconds * 0.5f;
            }

            return 0.0f;
        }
        /// <summary>
        /// Called when the server is sending a time sync package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public override void OnBeginSend(IServer server, TimeSyncPackage package, IConnection receiver)
        {
            this.StartWatch(receiver);
        }
        /// <summary>
        /// Called when the server receives a time sync package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public override void OnReceive(IServer server, TimeSyncPackage package, IConnection sender)
        {
            float latency = this.GetLatency(sender);
            sender.Latency = latency;

            logger.Trace("Setting latency for {0} to {1}ms.", sender.Address, latency);
            server.Send(new LatencyPackage { Latency = latency }, sender);
        }
        #endregion
    }
}
