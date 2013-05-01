using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D10;
using SharpDX.DXGI;

using System;
using System.Windows.Forms;

using Device1 = SharpDX.Direct3D10.Device1;
using DriverType = SharpDX.Direct3D10.DriverType;
using Factory = SharpDX.DXGI.Factory;
using D2DFactory = SharpDX.Direct2D1.Factory;
using FeatureLevel = SharpDX.Direct3D10.FeatureLevel;
using DXColor = SharpDX.Color;

using Rectangle = Xemio.GameLibrary.Math.Rectangle;
using Vector2 = Xemio.GameLibrary.Math.Vector2;

namespace Xemio.GameLibrary.Rendering.SharpDX
{
    internal class SharpDXRenderManager : IRenderManager
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpDXRenderManager"/> class.
        /// </summary>
        /// <param name="graphicsProvider">The graphics provider.</param>
        public SharpDXRenderManager(SharpDXGraphicsProvider graphicsProvider)
        {
            this.GraphicsProvider = graphicsProvider;      
            this.ScreenOffset = Math.Vector2.Zero;
            
            this.InitializeDirect2D(1, 1);
            this.GraphicsDevice.ResolutionChanged += GraphicsDeviceResolutionChanged;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return this.GraphicsProvider.GraphicsDevice; }
        }
        /// <summary>
        /// Gets the graphics provider.
        /// </summary>
        public IGraphicsProvider GraphicsProvider { get; private set; }
        /// <summary>
        /// Gets the screen offset.
        /// </summary>
        public Math.Vector2 ScreenOffset { get; private set; }
        #endregion

        #region Fields
        /// <summary>
        /// Backbuffer texture.
        /// </summary>
        private Texture2D _backBuffer;
        /// <summary>
        /// The tint color.
        /// </summary>
        private Color4 _tintColor;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the render manager.
        /// </summary>
        public void InitializeDirect2D(int width, int height)
        {
            // Display mode description
            ModeDescription mode = new ModeDescription(
                width,
                height,
                new Rational(60, 1),
                Format.R8G8B8A8_UNorm);

            // SwapChain description
            var description = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = mode,
                IsWindowed = true,
                OutputHandle = this.GraphicsDevice.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Create device and swapchain
            Device1 device;
            SwapChain swapChain;

            Device1.CreateWithSwapChain(
                DriverType.Hardware, 
                DeviceCreationFlags.BgraSupport, 
                description, 
                FeatureLevel.Level_10_0,
                out device, 
                out swapChain);

            // InitializeDirect2D backbuffer and surface
            this._backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            RenderTargetView renderView = new RenderTargetView(device, this._backBuffer);
            Surface surface = this._backBuffer.QueryInterface<Surface>();

            // New Direct2D factory
            SharpDXHelper.Factory2D = new D2DFactory();

            // new direct2d render target
            SharpDXHelper.RenderTarget = new RenderTarget(
                SharpDXHelper.Factory2D,
                surface,
                new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)
                ));

            SharpDXHelper.RenderTarget.AntialiasMode = AntialiasMode.Aliased;
            SharpDXHelper.SwapChain = swapChain;

            this.BeginDraw();
        }
        /// <summary>
        /// Begins the drawing process.
        /// </summary>
        private void BeginDraw()
        {
            SharpDXHelper.RenderTarget.BeginDraw();
        }
        /// <summary>
        /// Ends the drawing process.
        /// </summary>
        private void EndDraw()
        {
            SharpDXHelper.RenderTarget.EndDraw();
        }
        #endregion

        #region Implementation of IRenderManager
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
        /// Renders a the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The original destination.</param>
        public void Render(ITexture texture, Math.Rectangle destination, Math.Rectangle origin)
        {
            SharpDXTexture dxTexture = texture as SharpDXTexture;
            if (dxTexture == null)
            {
                throw new ArgumentException("Texture has to be SharpDXTexture.");
            }

            const BitmapInterpolationMode interpolation = BitmapInterpolationMode.NearestNeighbor;

            SharpDXHelper.RenderTarget.DrawBitmap(
                dxTexture.Bitmap,
                SharpDXHelper.ConvertRectangle(destination + this.ScreenOffset),
                this._tintColor.Alpha,
                interpolation,
                SharpDXHelper.ConvertRectangle(origin));
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
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Vector2 position, Color color)
        {
            this.Tint(color);

            this.Render(texture, position);
            this.Tint(Color.White);
        }
        /// <summary>
        /// Tints the screen.
        /// </summary>
        /// <param name="color"></param>
        public void Tint(Color color)
        {
            this._tintColor = SharpDXHelper.CreateColor(color);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public void Render(ITexture texture, Math.Rectangle destination)
        {
            this.Render(texture, destination, new Math.Rectangle(0, 0, texture.Width, texture.Height));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public void Render(ITexture texture, Math.Vector2 position)
        {
            this.Render(texture, new Math.Rectangle(position.X, position.Y, texture.Width, texture.Height));
        }
        /// <summary>
        /// Presents the backbuffer to the screen.
        /// </summary>
        public void Present()
        {
            this.EndDraw();
            SharpDXHelper.SwapChain.Present(0, PresentFlags.None);
            this.BeginDraw();
        }
        /// <summary>
        /// Clears the render target.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            SharpDXHelper.RenderTarget.Clear(new DXColor(color.R, color.G, color.B, color.A));
        }
        /// <summary>
        /// Offsets the screen.
        /// </summary>
        /// <param name="translation">The translation.</param>
        public void Translate(Vector2 translation)
        {
            this.ScreenOffset = translation;
        }
        /// <summary>
        /// Set rotation (NOT IMPLEMENTED).
        /// </summary>
        /// <param name="rotation"></param>
        public void Rotate(float rotation)
        {
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles a resize event.
        /// </summary>
        private void GraphicsDeviceResolutionChanged(object sender, EventArgs e)
        {
            this.InitializeDirect2D(this.GraphicsDevice.DisplayMode.Width, this.GraphicsDevice.DisplayMode.Height);
        }
        #endregion
    }
}
