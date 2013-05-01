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
        #region Implementation of IShape
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds { get; set; }
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
