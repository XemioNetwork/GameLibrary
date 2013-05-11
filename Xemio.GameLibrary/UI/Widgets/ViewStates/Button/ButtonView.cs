using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.Widgets.Base;
using Xemio.GameLibrary.UI.Widgets.View;

namespace Xemio.GameLibrary.UI.Widgets.ViewStates.Button
{
    public class ButtonView : WidgetView
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonView"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public ButtonView(Widget widget) : base(widget)
        {
            this.States.Add(new ButtonStateNormal());
            this.States.Add(new ButtonStateHover());
        }
        #endregion
    }
}
