using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.UI.Shapes
{
    public class RectangleShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        public RectangleShape()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public RectangleShape(Rectangle bounds)
        {
            this.Bounds = bounds;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RectangleShape(float x, float y, float width, float height)
        {
            this.Bounds = new Rectangle(x, y, width, height);
        }
        #endregion

        #region Implementation of IShape
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds { get; set; }
        /// <summary>
        /// Gets the position mode.
        /// </summary>
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// Fills the shape.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="brush">The brush.</param>
        public void Fill(IGeometryProvider geometry, IBrush brush)
        {
            geometry.FillRectangle(brush, this.Bounds);
        }
        /// <summary>
        /// Draws the shape.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="pen">The pen.</param>
        public void Draw(IGeometryProvider geometry, IPen pen)
        {
            geometry.DrawRectangle(pen, this.Bounds);
        }
        #endregion
    }
}
