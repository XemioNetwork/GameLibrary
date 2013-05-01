using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Xemio.GameLibrary.UI.Events
{
    public class KeyEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class.
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        public KeyEventArgs(Keys keyCode)
        {
            this.KeyCode = keyCode;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the key code.
        /// </summary>
        public Keys KeyCode { get; private set; }
        #endregion
    }
}
