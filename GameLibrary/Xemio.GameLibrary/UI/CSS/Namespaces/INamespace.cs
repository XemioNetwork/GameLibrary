using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.Widgets;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.CSS.Namespaces
{
    public interface INamespace
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the depth.
        /// </summary>
        int Depth { get; }
        /// <summary>
        /// Determines whether the namespace contains the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        bool Contains(Widget widget);
    }
}
