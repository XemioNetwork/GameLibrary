using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Common
{
    public class ThreadInvoker : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="ThreadInvoker"/> class.
        /// </summary>
        public ThreadInvoker()
        {
            this._dispatcher = Dispatcher.CurrentDispatcher;
        }
        #endregion

        #region Fields
        private readonly Dispatcher _dispatcher;
        #endregion

        #region Methods
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Invoke(Action action)
        {
            this._dispatcher.Invoke(action, null);
        }
        #endregion
    }
}
