using System;
using System.Windows.Forms;

namespace Xemio.GameLibrary.Common
{
    public class ThreadInvoker : IThreadInvoker
    {
        #region Fields
        private Control _invokerControl;
        #endregion

        #region Methods
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Invoke(Action action)
        {
            // Searching for a better method to invoke an action
            // inside the main application thread. There is no
            // other solution doing that, even though the implementation
            // is not really good it has to stay like that.

            if (this._invokerControl == null)
            {
                this._invokerControl = new Control();
                this._invokerControl.CreateControl();
            }

            if (this._invokerControl.IsHandleCreated && !this._invokerControl.IsDisposed)
            {
                this._invokerControl.Invoke(action);
            }
        }
        #endregion
    }
}
