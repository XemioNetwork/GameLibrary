using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.Textures;

namespace Xemio.GameLibrary.Rendering
{
    [AbstractComponent]
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
        /// <param name="translation">The translation.</param>
        void Translate(Vector2 translation);
        /// <summary>
        /// Sets the rotation to the specified angle in radians.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        void Rotate(float rotation);
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
    public static class RenderManagerOverloads
    {
        #region Methods
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public static void Render(this IRenderManager renderManager, ITexture texture, Vector2 position)
        {
            renderManager.Render(texture, position, Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public static void Render(this IRenderManager renderManager, ITexture texture, Vector2 position, Color color)
        {
            renderManager.Render(texture, new Rectangle(position.X, position.Y, texture.Width, texture.Height), color);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public static void Render(this IRenderManager renderManager, ITexture texture, Rectangle destination)
        {
            renderManager.Render(texture, destination, Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="color">The color.</param>
        public static void Render(this IRenderManager renderManager, ITexture texture, Rectangle destination, Color color)
        {
            renderManager.Render(texture, destination, new Rectangle(0, 0, texture.Width, texture.Height), color);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public static void Render(this IRenderManager renderManager, ITexture texture, Rectangle destination, Rectangle origin)
        {
            renderManager.Render(texture, destination, origin, Color.White);
        }
        #endregion
    }
}
