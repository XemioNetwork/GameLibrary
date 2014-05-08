using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    public class PolygonShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonShape"/> class.
        /// </summary>
        internal protected PolygonShape()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the vertices.
        /// </summary>
        public virtual Vector2[] Vertices { get; set; }
        #endregion

        #region Implementation of IShape
        /// <summary>
        /// Gets the position.
        /// </summary>
        public virtual Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the outline pen.
        /// </summary>
        public virtual IPen Outline { get; set; }
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        public virtual IBrush Background { get; set; }
        /// <summary>
        /// Renders the shape.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public virtual void Render(GraphicsDevice graphicsDevice)
        {
            if (this.Background != null)
            {
                graphicsDevice.FillPolygon(this.Background, this.Vertices);
            }
            if (this.Outline != null)
            {
                graphicsDevice.DrawPolygon(this.Outline, this.Vertices);
            }
        }
        #endregion
    }
}
