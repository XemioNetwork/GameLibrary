using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.Rendering
{
    public interface IWidgetRenderer
    {
        /// <summary>
        /// Renders the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        void Render(Widget widget);
    }
}
