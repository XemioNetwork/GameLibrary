using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets.Base;
using Xemio.GameLibrary.UI.Widgets.View;

namespace Xemio.GameLibrary.UI.Widgets.ViewStates.Button
{
    public class ButtonStateHover : WidgetViewState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonStateNormal"/> class.
        /// </summary>
        public ButtonStateHover() : base(WidgetState.Hover)
        {
            GraphicsDevice graphicsDevice = XGL.GetComponent<GraphicsDevice>();
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
