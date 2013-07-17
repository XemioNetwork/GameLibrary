using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.View.Instructions
{
    public interface IWidgetViewInstruction
    {
        /// <summary>
        /// Gets the shape.
        /// </summary>
        IShape Shape { get; }
        /// <summary>
        /// Renders this item.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void Render(WidgetGraphics graphics);
    }
}
