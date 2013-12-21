using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class PackageQueue : Worker
    {
        #region Fields
        private readonly ConcurrentQueue<PackageQueueItem> _queue; 
        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageQueue"/> class.
        /// </summary>
        public PackageQueue()
        {
            this._queue = new ConcurrentQueue<PackageQueueItem>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void Offer(Package package, ISender receiver)
        {
            this._queue.Enqueue(new PackageQueueItem(package, receiver));
            this._waitHandle.Set();
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
                this._waitHandle.WaitOne();

                PackageQueueItem packageQueueItem;
                while (this._queue.TryDequeue(out packageQueueItem))
                {
                    packageQueueItem.Receiver.Send(packageQueueItem.Package);
                }
            }
        }
        #endregion
    }
}
