using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.UI.Shapes
{
    public interface IShape
    {
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        Rectangle Bounds { get; set; }
        /// <summary>
        /// Fills the shape.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="brush">The brush.</param>
        void Fill(IGeometryProvider geometry, IBrush brush);
        /// <summary>
        /// Draws the shape.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="pen">The pen.</param>
        void Draw(IGeometryProvider geometry, IPen pen);
    }
}
