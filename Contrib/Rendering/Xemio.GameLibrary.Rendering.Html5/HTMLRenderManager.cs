using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.HTML5
{
    public class HTMLRenderManager : IRenderManager
    {
        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Require<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the screen offset.
        /// </summary>
        public Vector2 ScreenOffset { get; private set; }
        /// <summary>
        /// Gets the context.
        /// </summary>
        public dynamic Context
        {
            get { return ((HTMLRenderTarget)this.GraphicsDevice.RenderTarget).Context; }
        }
        #endregion
        
        #region Implementation of IRenderManager
        /// <summary>
        /// Clears the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.Context.fillStyle = HTMLHelper.Convert(color);
            this.Context.fillRect(0, 0, this.GraphicsDevice.DisplayMode.Width, this.GraphicsDevice.DisplayMode.Height);
        }
        /// <summary>
        /// Translates the specified translation.
        /// </summary>
        /// <param name="translation">The translation.</param>
        public void Translate(Vector2 translation)
        {
            this.ScreenOffset = translation;
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin, Color color)
        {
            HTMLTexture htmlTexture = (HTMLTexture) texture;
            if (htmlTexture == null)
            {
                throw new NotSupportedException("The HTML renderer only supports HTML textures.");
            }

            this.Context.drawImage(htmlTexture.Texture,
                (int)origin.X,
                (int)origin.Y,
                (int)origin.Width,
                (int)origin.Height,
                (int)destination.X, 
                (int)destination.Y,
                (int)destination.Width,
                (int)destination.Height);
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            WebSurface surface = XGL.Components.Require<WebSurface>();
            surface.Canvas.drawImage(
                this.Context,
                0, 0, surface.Width, surface.Height);
        }
        #endregion
    }
}
