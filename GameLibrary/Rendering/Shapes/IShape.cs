using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    public interface IShape
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the outline pen.
        /// </summary>
        IPen Outline { get; set; }
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        IBrush Background { get; set; }
        /// <summary>
        /// Renders the shape.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        void Render(GraphicsDevice graphicsDevice);
    }
}
