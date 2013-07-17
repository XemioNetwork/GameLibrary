using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.UI.Shapes
{
    public class RoundedRectangleShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        public RoundedRectangleShape()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="radius">The radius.</param>
        public RoundedRectangleShape(Rectangle bounds, int radius)
        {
            this.Bounds = bounds;
            this.Radius = radius;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the edge radius.
        /// </summary>
        public int Radius { get; set; }
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
            geometry.FillRoundedRectangle(brush, this.Bounds, this.Radius);
        }
        /// <summary>
        /// Draws the shape.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="pen">The pen.</param>
        public void Draw(IGeometryProvider geometry, IPen pen)
        {
            geometry.DrawRoundedRectangle(pen, this.Bounds, this.Radius);
        }
        #endregion
    }
}
