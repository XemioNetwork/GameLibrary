using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.View.Instructions
{
    internal class FillShapeInstruction : IWidgetViewInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FillShapeInstruction"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="brush">The brush.</param>
        public FillShapeInstruction(IShape shape, IBrush brush)
        {
            this.Shape = shape;
            this.Brush = brush;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the brush.
        /// </summary>
        public IBrush Brush { get; private set; }
        #endregion

        #region Implementation of IWidgetViewItem
        /// <summary>
        /// Gets the shape.
        /// </summary>
        public IShape Shape { get; private set; }
        /// <summary>
        /// Renders this item.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        public void Render(WidgetGraphics graphics)
        {
            this.Shape.Fill(graphics, this.Brush);
        }
        #endregion
    }
}
