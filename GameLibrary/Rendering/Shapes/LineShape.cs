using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    public class LineShape : IShape
    {
        #region Properties
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public Vector2 End
        {
            get { return this.Position + this.Direction; }
            set { this.Direction = value - this.Position; }
        }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public virtual Vector2 Direction { get; set; }
        #endregion

        #region Implementation of IShape
        /// <summary>
        /// Gets the position.
        /// </summary>
        public virtual Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        IBrush IShape.Background
        {
            get { return default(IBrush); }
            set { throw new NotSupportedException("Setting the background of a LineShape is not supported."); }
        }
        /// <summary>
        /// Gets or sets the outline pen.
        /// </summary>
        public virtual IPen Outline { get; set; }
        /// <summary>
        /// Renders the shape.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public virtual void Render(GraphicsDevice graphicsDevice)
        {
            if (this.Outline != null)
            {
                graphicsDevice.DrawLine(this.Outline, this.Position, this.End);
            }
        }
        #endregion
    }
}
