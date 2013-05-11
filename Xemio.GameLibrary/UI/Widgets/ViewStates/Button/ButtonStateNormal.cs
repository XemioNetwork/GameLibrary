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
using Xemio.GameLibrary.UI.Widgets.View.Instructions;

namespace Xemio.GameLibrary.UI.Widgets.ViewStates.Button
{
    public class ButtonStateNormal : WidgetViewState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonStateNormal"/> class.
        /// </summary>
        public ButtonStateNormal() : base(WidgetState.Normal)
        {
            GraphicsDevice graphicsDevice = XGL.GetComponent<GraphicsDevice>();
            IGeometryFactory factory = graphicsDevice.Geometry.Factory;

            IShape backgroundShape = new RectangleShape(0, 0, 1, 1);
            IBrush backgroundBrush = factory.CreateSolid(Color.Red);
            IPen backgroundPen = factory.CreatePen(Color.White);

            IShape glassShape = new RectangleShape(0, 0, 1, 0.5f);
            IBrush glassBrush = factory.CreateSolid(Color.Lerp(Color.Red, Color.White, 0.2f));

            this.Fill(backgroundShape, backgroundBrush);
            this.Fill(glassShape, glassBrush);
            this.Draw(backgroundShape, backgroundPen);
        }
        #endregion
    }
}
