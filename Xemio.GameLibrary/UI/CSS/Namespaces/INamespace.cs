using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.CSS.Namespaces
{
    public interface INamespace
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Determines whether the namespace contains the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        bool Contains(Widget widget);
    }
}
