using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.SDL
{
    public class SDLRenderManager : IRenderManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SDLRenderManager"/> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public SDLRenderManager(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
            this.GraphicsDevice.ResolutionChanged += GraphicsDeviceOnResolutionChanged;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the display offset.
        /// </summary> 
        public Vector2 Translation { get; private set; }
        /// <summary>
        /// Gets the surface.
        /// </summary>
        public Surface Surface
        {
            get 
            {
                var target = this.GraphicsDevice.RenderTarget as SDLRenderTarget;
                if (target == null)
                {
                    throw new InvalidOperationException("No active SDL render target.");
                }

                return target.Surface;
            }
        }
        #endregion

        #region Implementation of IRenderManager
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
        /// <summary>
        /// Gets the back buffer.
        /// </summary>
        public IRenderTarget BackBuffer { get; private set; }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.Surface.Fill(SDLHelper.Convert(color));
        }
        /// <summary>
        /// Offsets the screen.
        /// </summary>
        /// <param name="translation">The translation.</param>
        public void Translate(Vector2 translation)
        {
            this.Translation = translation;
        }
        /// <summary>
        /// Sets the rotation to the specified angle in radians.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        public void Rotate(float rotation)
        {
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public void Render(ITexture texture, Vector2 position)
        {
            this.Render(texture, position, Color.Black);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Vector2 position, Color color)
        {
            SDLTexture sdlTexture = texture as SDLTexture;

            this.Surface.Blit(
                sdlTexture.Surface,
                SDLHelper.Convert(position + this.Translation));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public void Render(ITexture texture, Rectangle destination)
        {
            this.Render(texture, destination, Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Rectangle destination, Color color)
        {
            SDLTexture sdlTexture = texture as SDLTexture;

            this.Surface.Blit(
                sdlTexture.Surface,
                SDLHelper.Convert(destination + this.Translation));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
            this.Render(texture, destination, origin, Color.White);
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
            SDLTexture sdlTexture = texture as SDLTexture;

            this.Surface.Blit(
                sdlTexture.Surface,
                SDLHelper.Convert(destination + this.Translation),
                SDLHelper.Convert(origin));
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            Control surface = Control.FromHandle(this.GraphicsDevice.Handle);

            if (surface != null)
            {
                int screenWidth = surface.ClientSize.Width;
                int screenHeight = surface.ClientSize.Height;

                System.Drawing.Graphics graphics = surface.CreateGraphics();
                var bitmap = this.Surface.Bitmap;

                IntPtr hdc = graphics.GetHdc();
                IntPtr compatibleDC = GDIHelper.CreateCompatibleDC(hdc);
                IntPtr bufferDC = bitmap.GetHbitmap();

                GDIHelper.SelectObject(compatibleDC, bufferDC);

                GDIHelper.StretchBlt
                (
                    hdc, 0, 0,
                    screenWidth,
                    screenHeight,
                    compatibleDC,
                    0, 0,
                    this.GraphicsDevice.DisplayMode.Width,
                    this.GraphicsDevice.DisplayMode.Height,
                    GDIRasterOperations.SRCCOPY
                );

                GDIHelper.DeleteObject(bufferDC);
                GDIHelper.DeleteObject(compatibleDC);

                graphics.ReleaseHdc(hdc);
            }

            this.Translation = Vector2.Zero;
            this.Clear(Color.Black);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles a resolution change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GraphicsDeviceOnResolutionChanged(object sender, EventArgs eventArgs)
        {
            var graphicsDevice = sender as GraphicsDevice;

            int width = graphicsDevice.DisplayMode.Width;
            int height = graphicsDevice.DisplayMode.Height;

            this.BackBuffer = new SDLRenderTarget(new Surface(width, height, 32, true));
        }
        #endregion
    }
}
