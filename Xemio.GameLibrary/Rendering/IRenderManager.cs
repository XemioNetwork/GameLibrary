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
        /// Gets the graphics device.
        /// </summary>
        GraphicsDevice GraphicsDevice { get; }
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
        /// Sets the rotation to the specified angle in radians.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        void Rotate(float rotation);
        /// <summary>
        /// Tints all drawn images using the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        void Tint(Color color);
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
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        void Render(ITexture texture, Vector2 position, Color color);
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
        void Render(ITexture texture, Rectangle destination, Color color);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        void Render(ITexture texture, Rectangle destination, Rectangle origin);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="color">The color.</param>
        void Render(ITexture texture, Rectangle destination, Rectangle origin, Color color);
        /// <summary>
        /// Presents this instance.
        /// </summary>
        void Present();
    }
}
