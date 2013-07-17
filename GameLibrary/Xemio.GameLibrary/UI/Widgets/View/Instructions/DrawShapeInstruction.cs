using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.View.Instructions
{
    internal class DrawShapeInstruction : IWidgetViewInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawShapeInstruction"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="pen">The pen.</param>
        public DrawShapeInstruction(IShape shape, IPen pen)
        {
            this.Shape = shape;
            this.Pen = pen;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the pen.
        /// </summary>
        public IPen Pen { get; private set; }
        #endregion

        #region Implementation of IWidgetViewItem
        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        public IShape Shape { get; private set; }
        /// <summary>
        /// Renders this item.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        public void Render(WidgetGraphics graphics)
        {
            this.Shape.Draw(graphics, this.Pen);
        }
        #endregion
    }
}
