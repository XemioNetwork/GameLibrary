using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Xemio.GameLibrary.Common
{
    public static class ThreadInvoker
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="ThreadInvoker"/> class.
        /// </summary>
        static ThreadInvoker()
        {
            ThreadInvoker.LazyInitalize();
        }
        #endregion

        #region Fields
        private static Control _invokerControl;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the thread invoker.
        /// </summary>
        private static void LazyInitalize()
        {
            if (_invokerControl == null)
            {
                _invokerControl = new Control();
                _invokerControl.CreateControl();
            }
        }
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Invoke(Action action)
        {
            if (_invokerControl.IsHandleCreated && !_invokerControl.IsDisposed)
            {
                _invokerControl.Invoke(action);
            }
        }
        #endregion
    }
}
