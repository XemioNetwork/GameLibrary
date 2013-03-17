using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.Xna
{
    using Xna = Microsoft.Xna.Framework;
    using XemioGraphicsDevice = Xemio.GameLibrary.Rendering.GraphicsDevice;

    public class XnaRenderManager : IRenderManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaRenderManager"/> class.
        /// </summary>
        /// <param name="graphicsProvider">The graphics provider.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        public XnaRenderManager(XnaGraphicsProvider graphicsProvider, XemioGraphicsDevice graphicsDevice)
        {
            this._color = Xna.Color.White;
            this._graphicsDevice = graphicsDevice;

            this.GraphicsProvider = graphicsProvider;
        }
        #endregion

        #region Fields
        private XemioGraphicsDevice _graphicsDevice;

        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        private Xna.Color _color;
        private Xna.Vector2 _offset;

        private float _rotation;
        #endregion

        #region Methods
        /// <summary>
        /// Begins the rendering process
        /// </summary>
        private void Begin()
        {
            this._spriteBatch.Begin(
                SpriteSortMode.Deferred,
                Xna.Graphics.BlendState.NonPremultiplied,
                SamplerState.PointClamp, null, null);
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            if (this._spriteBatch == null)
            {
                XemioGraphicsDevice device = XGL.GetComponent<XemioGraphicsDevice>();

                this._renderTarget = new RenderTarget2D(
                    XnaHelper.GraphicsDevice, device.DisplayMode.Width, device.DisplayMode.Height);

                XnaHelper.GraphicsDevice.SetRenderTarget(this._renderTarget);
                XnaHelper.GraphicsDevice.Clear(Xna.Color.Black);

                this._spriteBatch = new SpriteBatch(XnaHelper.GraphicsDevice);
                this._spriteBatch.Begin(SpriteSortMode.Deferred, Xna.Graphics.BlendState.NonPremultiplied);
            }
        }
        #endregion

        #region IRenderProvider Member
        /// <summary>
        /// Gets the graphics provider.
        /// </summary>
        public IGraphicsProvider GraphicsProvider { get; private set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public XemioGraphicsDevice GraphicsDevice
        {
            get { return this.GraphicsProvider.GraphicsDevice; }
        }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            XnaHelper.GraphicsDevice.Clear(new Xna.Color(color.R, color.G, color.B, color.A));
        }
        /// <summary>
        /// Offsets the screen.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public void Offset(Vector2 offset)
        {
            this._offset = new Xna.Vector2(offset.X, offset.Y);
        }
        /// <summary>
        /// Sets the rotation to the specified angle in radians.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        public void Rotate(float rotation)
        {
            this._rotation = rotation;
        }
        /// <summary>
        /// Tints all drawn images using the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Tint(Color color)
        {
            this._color = new Xna.Color(color.R, color.G, color.B, color.A);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public void Render(ITexture texture, Vector2 position)
        {
            this.Render(texture, new Rectangle(position.X, position.Y, texture.Width, texture.Height));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public void Render(ITexture texture, Rectangle destination)
        {
            this.Render(texture, destination, new Rectangle(0, 0, texture.Width, texture.Height));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Vector2 position, Color color)
        {
            this.Tint(color);
            this.Render(texture, position);

            this.Tint(Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Rectangle destination, Color color)
        {
            this.Tint(color);
            this.Render(texture, destination);

            this.Tint(Color.White);
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
            this.Tint(color);
            this.Render(texture, destination, origin);

            this.Tint(Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
            this.Initialize();

            XnaTexture xnaTexture = texture as XnaTexture;
            if (xnaTexture == null)
            {
                throw new InvalidOperationException("You have to use a xna texture.");
            }

            Xna.Vector2 position = new Xna.Vector2(destination.X, destination.Y);
            Xna.Vector2 scale = Xna.Vector2.One;

            this._spriteBatch.Draw(
                xnaTexture.Texture,
                new Xna.Rectangle(
                    (int)destination.X + (int)this._offset.X,
                    (int)destination.Y + (int)this._offset.Y,
                    (int)destination.Width,
                    (int)destination.Height),
                new Xna.Rectangle(
                    (int)origin.X, 
                    (int)origin.Y,
                    (int)origin.Width,
                    (int)origin.Height),
                this._color, this._rotation,
                Xna.Vector2.Zero, SpriteEffects.None, 0);
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            this.Initialize();
            this._spriteBatch.End();

            XnaHelper.GraphicsDevice.SetRenderTarget(null);
            XemioGraphicsDevice graphicsDevice = XGL.GetComponent<XemioGraphicsDevice>();

            this.Begin();

            this._spriteBatch.Draw(
                this._renderTarget,
                new Xna.Rectangle(0, 0, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height),
                Xna.Color.White);

            this._spriteBatch.End();

            XnaHelper.GraphicsDevice.Present();

            XnaHelper.GraphicsDevice.SetRenderTarget(this._renderTarget);
            XnaHelper.GraphicsDevice.Clear(Xna.Color.Black);

            this.Begin();
        }
        #endregion
    }
}
