using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.Widgets.Base;
using Xemio.GameLibrary.UI.Widgets.View.ViewStates.Button;

namespace Xemio.GameLibrary.UI.Widgets
{
    public class Button : Widget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
        {
            this.View = new ButtonView(this);
        }
        #endregion
    }
}
