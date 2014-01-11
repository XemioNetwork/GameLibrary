using System.Threading;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common.Threads
{
    public abstract class Worker
    {
        #region Fields
        private bool _running;
        private Task _task;
        private readonly CancellationTokenSource _source;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        protected Worker()
        {
            this._source = new CancellationTokenSource();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this worker is running.
        /// </summary>
        public bool IsRunning()
        {
            return this._running && this._source.IsCancellationRequested == false;
        }
        /// <summary>
        /// Starts this worker.
        /// </summary>
        public virtual void Start()
        {
            if (!this.IsRunning())
            {
                this._running = true;
                this._task = Task.Factory.StartNew(this.Run, this._source.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }
        /// <summary>
        /// Interrupts this worker.
        /// </summary>
        public virtual void Interrupt()
        {
            if (this.IsRunning())
            {
                this._running = false;
                this._source.Cancel(false);
            }
        }
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected abstract void Run();
        #endregion
    }
}
