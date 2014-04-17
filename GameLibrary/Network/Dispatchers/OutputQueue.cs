using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using NLog;
using Xemio.GameLibrary.Common.Threads;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Events.Servers;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network.Dispatchers
{
    internal abstract class OutputQueue : Worker
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly ISender _receiver;
        private readonly ConcurrentQueue<Package> _queue; 

        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputQueue" /> class.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        protected OutputQueue(ISender receiver) : base(ThreadStartBehavior.AutoStart)
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

        #region Event Methods
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="package">The package.</param>
        protected virtual void OnSendingPackage(Package package)
        {
        }
        /// <summary>
        /// Called when the connection was lost.
        /// </summary>
        protected virtual void OnLostConnection()
        {
        }
        #endregion

        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            while (this.IsRunning && this._receiver.Connected)
            {
                if (this._waitHandle.WaitOne(TimeSpan.FromSeconds(1)))
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

                            this.OnSendingPackage(package);
                            this._receiver.Send(package);

                            processedPackageCount++;
                        }
                    }
                    catch (ChannelLostConnectionException ex)
                    {
                        logger.Warn("Did not deliver package to receiver: Channel closed.");
                        this.OnLostConnection();

                        break;
                    }
                    catch (ClientLostConnectionException ex)
                    {
                        logger.Warn("Did not deliver package to receiver: Connection to server lost.");
                        this.OnLostConnection();

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
