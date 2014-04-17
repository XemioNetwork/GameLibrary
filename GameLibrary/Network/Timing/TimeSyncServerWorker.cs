using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Threads;

namespace Xemio.GameLibrary.Network.Timing
{
    public class TimeSyncServerWorker : Worker
    {
        #region Fields
        private readonly Server _server;
        private readonly int _syncInterval;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSyncServerWorker" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="syncInterval">The synchronize interval.</param>
        public TimeSyncServerWorker(Server server, int syncInterval) : base(ThreadStartBehavior.AutoStart)
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
            while (this.IsRunning)
            {
                var syncPackage = new TimeSyncPackage();
                this._server.Send(syncPackage);

                Thread.Sleep(this._syncInterval);
            }
        }
        #endregion
    }
}
