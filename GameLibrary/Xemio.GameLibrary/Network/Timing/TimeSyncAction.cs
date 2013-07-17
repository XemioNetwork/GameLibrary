using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Xemio.GameLibrary.Network.Subscribers;

namespace Xemio.GameLibrary.Network.Timing
{
    public class TimeSyncAction : ActionSubscriber<TimeSyncPackage>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSyncAction"/> class.
        /// </summary>
        public TimeSyncAction()
        {
            this._firstSync = true;
            this._elapsed = 0;

            this._watches = new Dictionary<IConnection, Stopwatch>();
            this.SyncDelay = 5000;
        }
        #endregion

        #region Fields
        private bool _firstSync;
        private float _elapsed;

        private Dictionary<IConnection, Stopwatch> _watches;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sync delay.
        /// </summary>
        public float SyncDelay { get; set; }
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
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            if (this._elapsed >= this.SyncDelay || this._firstSync)
            {
                this._elapsed = 0;
                this._firstSync = false;

                Server server = XGL.GetComponent<Server>();
                TimeSyncPackage syncPackage = new TimeSyncPackage();

                server.Send(syncPackage);
            }
        }
        /// <summary>
        /// Called when the server is sending a time sync package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public override void OnBeginSend(Server server, TimeSyncPackage package, IConnection receiver)
        {
            this.StartWatch(receiver);
        }
        /// <summary>
        /// Called when the server receives a time sync package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public override void OnReceive(Server server, TimeSyncPackage package, IConnection sender)
        {
            float latency = this.GetLatency(sender);
            sender.Latency = latency;

            LatencyPackage latencyPackage = new LatencyPackage();
            latencyPackage.Latency = latency;

            server.Send(latencyPackage, sender);
        }
        #endregion
    }
}
