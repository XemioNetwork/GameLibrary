using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.UI.Shapes
{
    public class EllipseShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        public EllipseShape()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public EllipseShape(Rectangle bounds)
        {
            this.Bounds = bounds;
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
            geometry.FillEllipse(brush, this.Bounds);
        }
        /// <summary>
        /// Draws the shape.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="pen">The pen.</param>
        public void Draw(IGeometryProvider geometry, IPen pen)
        {
            geometry.DrawEllipse(pen, this.Bounds);
        }
        #endregion
    }
}
