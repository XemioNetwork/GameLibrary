using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D10;
using SharpDX.DXGI;

using System;
using System.Windows.Forms;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device1 = SharpDX.Direct3D10.Device1;
using DriverType = SharpDX.Direct3D10.DriverType;
using Factory = SharpDX.DXGI.Factory;
using D2DFactory = SharpDX.Direct2D1.Factory;
using FeatureLevel = SharpDX.Direct3D10.FeatureLevel;
using DXColor = SharpDX.Color;

using Rectangle = Xemio.GameLibrary.Math.Rectangle;
using Resource = SharpDX.Direct3D10.Resource;
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
        /// Gets the back buffer.
        /// </summary>
        public IRenderTarget BackBuffer
        {
            get { return null; }
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
        /// The device context.
        /// </summary>
        private DeviceContext _deviceContext;
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
            this._backBuffer = Resource.FromSwapChain<Texture2D>(swapChain, 0);
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

            this._deviceContext = new DeviceContext(surface);
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
            this.Render(texture, destination, new Rectangle(0, 0, texture.Width, texture.Height), color);
        }
        /// <summary>
        /// Renders a the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The original destination.</param>
        public void Render(ITexture texture, Math.Rectangle destination, Math.Rectangle origin)
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
            SharpDXTexture dxTexture = texture as SharpDXTexture;
            if (dxTexture == null)
            {
                throw new ArgumentException("Texture has to be SharpDXTexture.");
            }

            const BitmapInterpolationMode interpolation = BitmapInterpolationMode.NearestNeighbor;

            float a = color.A / 255.0f;
            float r = color.R / 255.0f;
            float g = color.G / 255.0f;
            float b = color.B / 255.0f;

            ColorMatrix colorMatrix = new ColorMatrix(this._deviceContext);

            Matrix5x4 matrix = Matrix5x4.Identity;
            //TODO: implement tinting, found nothing on google to solve this problem

            colorMatrix.Matrix = matrix;
            colorMatrix.AlphaMode = AlphaMode.Premultiplied;
            colorMatrix.SetInput(0, dxTexture.Bitmap, true);

            SharpDXHelper.RenderTarget.DrawBitmap(
                (Bitmap)colorMatrix.Output,
                SharpDXHelper.ConvertRectangle(destination + this.ScreenOffset),
                this._tintColor.Alpha,
                interpolation,
                SharpDXHelper.ConvertRectangle(origin));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Vector2 position, Color color)
        {
            this.Render(
                texture,
                new Rectangle(position.X, position.Y, texture.Width, texture.Height),
                new Rectangle(0, 0, texture.Width, texture.Height), color);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public void Render(ITexture texture, Math.Rectangle destination)
        {
            this.Render(texture,
                destination, 
                new Math.Rectangle(0, 0, texture.Width, texture.Height));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public void Render(ITexture texture, Math.Vector2 position)
        {
            this.Render(texture,
                new Math.Rectangle(position.X, position.Y, texture.Width, texture.Height));
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
