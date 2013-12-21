using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xemio.GameLibrary.Common
{
    public class ThreadInvoker : IThreadInvoker
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadInvoker"/> class.
        /// </summary>
        public ThreadInvoker()
        {
            if (SynchronizationContext.Current == null)
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            }

            this._factory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
        }
        #endregion

        #region Fields
        private readonly TaskFactory _factory;
        #endregion

        #region Methods
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Invoke(Action action)
        {
            this._factory.StartNew(action).Wait();
        }
        #endregion
    }
}
