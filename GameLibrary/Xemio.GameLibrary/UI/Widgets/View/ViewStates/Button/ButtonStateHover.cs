using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.View.ViewStates.Button
{
    internal class ButtonStateHover : WidgetViewState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonStateNormal"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public ButtonStateHover(Widget widget) : base(WidgetState.Hover)
        {
            GraphicsDevice graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            IGeometryFactory factory = graphicsDevice.Geometry.Factory;

            IShape backgroundShape = new RectangleShape(0, 0, 1, 1);
            IBrush backgroundBrush = factory.CreateSolid(Color.Blue);

            IShape glassShape = new RectangleShape(0, 0, 1, 0.5f);
            IBrush glassBrush = factory.CreateSolid(Color.Lerp(Color.Blue, Color.White, 0.1f));

            this.Fill(backgroundShape, backgroundBrush);
            this.Fill(glassShape, glassBrush);
        }
        #endregion
    }
}
