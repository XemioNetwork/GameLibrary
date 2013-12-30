using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class OutputQueue : Worker
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly ISender _receiver;
        private readonly ConcurrentQueue<Package> _queue; 

        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputQueue" /> class.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        public OutputQueue(ISender receiver)
        {
            this._receiver = receiver;
            this._queue = new ConcurrentQueue<Package>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Enqueue(Package package)
        {
            this._queue.Enqueue(package);
            this._waitHandle.Set();
        }
        #endregion
        
        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            while (this.IsRunning() && this._receiver.Connected)
            {
                if (this._waitHandle.WaitOne())
                {
                    Stopwatch watch = Stopwatch.StartNew();

                    int queueItemCount = this._queue.Count;
                    int processedPackageCount = 0;

                    logger.Trace("Detected {0} queued package items.", queueItemCount);
                    
                    try
                    {
                        Package package;
                        while (this._queue.TryDequeue(out package))
                        {
                            logger.Trace("Processing {0}.", package.GetType().Name);

                            this._receiver.Send(package);
                            processedPackageCount++;
                        }
                    }
                    catch (ConnectionClosedException ex)
                    {
                        logger.Warn("Did not deliver package to receiver: Connection closed.");
                        break;
                    }

                    watch.Stop();

                    if (processedPackageCount > 0)
                    {
                        logger.Trace("Processed {0} packages in {1}ms.", processedPackageCount, watch.Elapsed.TotalMilliseconds);
                    }
                }
            }
        }
        #endregion
    }
}
