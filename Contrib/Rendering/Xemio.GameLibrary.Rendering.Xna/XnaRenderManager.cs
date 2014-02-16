using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaRenderManager : IRenderManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaRenderManager"/> class.
        /// </summary>
        /// <param name="interpolation">The interpolation.</param>
        public XnaRenderManager(InterpolationMode interpolation)
        {
            this.InterpolationMode = interpolation;
            this.Begin();
        }
        #endregion

        #region Fields
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _currentRenderTarget;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Require<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the interpolation mode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; private set; }
        /// <summary>
        /// Gets or sets the screen offset.
        /// </summary>
        public Vector2 ScreenOffset { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Begins the rendering process.
        /// </summary>
        private void Begin()
        {
            if (this._spriteBatch == null)
            {
                this._spriteBatch = new SpriteBatch(XnaHelper.GraphicsDevice);
            }

            XnaHelper.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            this._spriteBatch.Begin(
                SpriteSortMode.Immediate, 
                BlendState.NonPremultiplied,
                XnaHelper.Convert(this.InterpolationMode), null, null);
        }
        /// <summary>
        /// Updates the render target.
        /// </summary>
        private void UpdateRenderTarget()
        {
            var xnaRenderTarget = (XnaRenderTarget)this.GraphicsDevice.RenderTarget;
            var renderTarget = xnaRenderTarget.RenderTarget;

            if (this._currentRenderTarget != renderTarget)
            {
                this._spriteBatch.End();
                XnaHelper.GraphicsDevice.SetRenderTarget(renderTarget);
                this.Begin();
            }

            this._currentRenderTarget = renderTarget;
        }
        #endregion

        #region Implementation of IRenderManager
        /// <summary>
        /// Clears the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.UpdateRenderTarget();
            XnaHelper.GraphicsDevice.Clear(XnaHelper.Convert(color));
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
            var xnaTexture = texture as XnaTexture;
            if (xnaTexture == null)
            {
                throw new InvalidOperationException("You have to use an XNA texture.");
            }

            this.UpdateRenderTarget();

            Rectangle translatedDestination = new Rectangle(
                (int)destination.X + (int)this.ScreenOffset.X,
                (int)destination.Y + (int)this.ScreenOffset.Y,
                (int)destination.Width,
                (int)destination.Height);

            this._spriteBatch.Draw(xnaTexture.Texture,
                                   XnaHelper.Convert(translatedDestination),
                                   XnaHelper.Convert(origin),
                                   XnaHelper.Convert(color));
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            var backBuffer = this.GraphicsDevice.BackBuffer as XnaRenderTarget;
            var texture = backBuffer.Texture;
            
            this._spriteBatch.End();
            this._currentRenderTarget = null;

            XnaHelper.GraphicsDevice.SetRenderTarget(null);
            XnaHelper.GraphicsDevice.Clear(XnaHelper.Convert(Color.Transparent));
            
            this.Begin();
            this._spriteBatch.Draw(texture,
                XnaHelper.Convert(Vector2.Zero),
                XnaHelper.Convert(Color.White));

            this._spriteBatch.End();

            XnaHelper.GraphicsDevice.Present();

            this.Begin();
            this.Clear(Color.Black);
        }
        /// <summary>
        /// Gets the back buffer.
        /// </summary>
        public IRenderTarget BackBuffer { get; private set; }
        #endregion
    }
}
