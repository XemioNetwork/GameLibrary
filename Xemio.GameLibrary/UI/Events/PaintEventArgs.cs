using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.Events
{
    public class PaintEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PaintEventArgs"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public PaintEventArgs(Widget widget)
        {
            this.Graphics = new WidgetGraphics(widget);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        public WidgetGraphics Graphics { get; private set; }
        #endregion
    }
}
