using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.View.ViewStates.Button
{
    internal class ButtonStateNormal : WidgetViewState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonStateNormal"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public ButtonStateNormal(Widget widget) : base(WidgetState.Normal)
        {
            GraphicsDevice graphicsDevice = XGL.GetComponent<GraphicsDevice>();
            IGeometryFactory factory = graphicsDevice.Geometry.Factory;

            IShape backgroundShape = new RectangleShape(0, 0, 1, 1);
            IBrush backgroundBrush = factory.CreateSolid(Color.Red);
            IPen backgroundPen = factory.CreatePen(Color.White);

            IShape glassShape = new RectangleShape(0, 0, 1, 0.5f);
            IBrush glassBrush = factory.CreateSolid(Color.Lerp(Color.Red, Color.White, 0.5f));

            this.Fill(backgroundShape, backgroundBrush);
            this.Fill(glassShape, glassBrush);
            this.Draw(backgroundShape, backgroundPen);
        }
        #endregion
    }
}
