using System.Threading;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common.Threads
{
    public abstract class Worker
    {
        #region Fields
        private bool _running;
        private readonly CancellationTokenSource _source;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Worker" /> class.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        protected Worker(ThreadStartBehavior behavior)
        {
            this._source = new CancellationTokenSource();
            if (behavior == ThreadStartBehavior.AutoStart)
            {
                this.Start();
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this worker is running.
        /// </summary>
        public bool IsRunning
        {
            get { return this._running && this._source.IsCancellationRequested == false; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Starts this worker.
        /// </summary>
        public virtual void Start()
        {
            if (!this.IsRunning)
            {
                this._running = true;
                Task.Factory.StartNew(this.Run, this._source.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }
        /// <summary>
        /// Interrupts this worker.
        /// </summary>
        public virtual void Interrupt()
        {
            if (this.IsRunning)
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
