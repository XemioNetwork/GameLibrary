using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common
{
    public abstract class Worker
    {
        #region Fields
        private bool _running;
        private Task _task;
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this worker is running.
        /// </summary>
        public bool IsRunning()
        {
            return this._running;
        }
        /// <summary>
        /// Starts this worker.
        /// </summary>
        public virtual void Start()
        {
            if (!this.IsRunning())
            {
                this._running = true;
                this._task = Task.Factory.StartNew(this.Run, TaskCreationOptions.LongRunning);
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
                this._task.Wait();
            }
        }
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected abstract void Run();
        #endregion
    }
}
