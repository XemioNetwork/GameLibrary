using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering
{
    public interface IRenderManager : IComponent
    {
        /// <summary>
        /// Gets the graphics provider.
        /// </summary>
        IGraphicsProvider GraphicsProvider { get; }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        void Clear(Color color);
        /// <summary>
        /// Offsets the screen.
        /// </summary>
        /// <param name="offset">The offset.</param>
        void Offset(Vector2 offset);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        void Render(ITexture texture, Vector2 position);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        void Render(ITexture texture, Rectangle destination);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        void Render(ITexture texture, Rectangle destination, Rectangle origin);
        /// <summary>
        /// Presents this instance.
        /// </summary>
        void Present();
    }
}
