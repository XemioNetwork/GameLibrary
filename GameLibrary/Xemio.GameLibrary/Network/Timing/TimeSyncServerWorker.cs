using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Network.Timing
{
    public class TimeSyncServerWorker : Worker
    {
        #region Fields
        private readonly IServer _server;
        private readonly int _syncInterval;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSyncServerWorker" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="syncInterval">The synchronize interval.</param>
        public TimeSyncServerWorker(IServer server, int syncInterval)
        {
            this._server = server;
            this._syncInterval = syncInterval;
        }
        #endregion

        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            while (this.IsRunning())
            {
                var syncPackage = new TimeSyncPackage();
                this._server.Send(syncPackage);

                Thread.Sleep(this._syncInterval);
            }
        }
        #endregion
    }
}
