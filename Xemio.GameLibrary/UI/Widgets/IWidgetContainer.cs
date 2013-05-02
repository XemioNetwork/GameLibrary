using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.UI.Widgets
{
    public interface IWidgetContainer
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        Vector2 Position { get; set; }
        /// <summary>
        /// Gets the absolute position.
        /// </summary>
        Vector2 AbsolutePosition { get; }
        /// <summary>
        /// Adds the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        void Add(Widget widget);
        /// <summary>
        /// Removes the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        void Remove(Widget widget);
        /// <summary>
        /// Gets the widgets.
        /// </summary>
        IEnumerable<Widget> Widgets { get; } 
    }
}
