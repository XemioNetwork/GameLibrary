using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.UI.Events
{
    public class MouseEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="button">The button.</param>
        public MouseEventArgs(Vector2 position, MouseButtons button)
        {
            this.Position = position;
            this.Button = button;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// Gets the button.
        /// </summary>
        public MouseButtons Button { get; private set; }
        #endregion
    }
}
