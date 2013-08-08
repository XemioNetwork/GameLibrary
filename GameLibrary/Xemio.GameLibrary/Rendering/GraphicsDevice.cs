using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Math;
using System.Windows.Forms;

namespace Xemio.GameLibrary.Rendering
{
    public class GraphicsDevice : IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsDevice"/> class.
        /// </summary>
        public GraphicsDevice(IntPtr handle)
        {
            this.Handle = handle;
            this.Targets = new Stack<IRenderTarget>();
        }
        #endregion

        #region Fields
        private DisplayMode _displayMode;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        public IntPtr Handle { get; set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public IGraphicsProvider Provider { get; set; }
        /// <summary>
        /// Gets the scale.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                Control control = Control.FromHandle(this.Handle);
                if (control == null)
                {
                    return new Vector2(1, 1);
                }

                float x = control.ClientSize.Width / (float)this.DisplayMode.Width;
                float y = control.ClientSize.Height / (float)this.DisplayMode.Height;

                return new Vector2(x, y);
            }
        }
        /// <summary>
        /// Gets or sets the geometry.
        /// </summary>
        public IGeometryProvider Geometry
        {
            get
            {
                if (this.Provider.IsGeometrySupported)
                {
                    return this.Provider.Geometry;
                }

                return GeometryProvider.Empty;
            }
        }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory
        {
            get { return this.Provider.TextureFactory; }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return this.Provider.RenderManager; }
        }
        /// <summary>
        /// Gets the display mode.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get { return this._displayMode; }
            set
            {
                if (this._displayMode != null)
                {
                    this.CreateBackBuffer(value);
                }

                this._displayMode = value;
                if (this.ResolutionChanged != null)
                {
                    this.ResolutionChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets the back buffer.
        /// </summary>
        public IRenderTarget BackBuffer { get; private set; }
        /// <summary>
        /// Gets the current render target.
        /// </summary>
        public IRenderTarget RenderTarget
        {
            get
            {
                if (this.Targets.Count > 0)
                    return this.Targets.First();

                return this.BackBuffer;
            }
        }
        /// <summary>
        /// Gets the render target stack.
        /// </summary>
        internal Stack<IRenderTarget> Targets { get; private set; }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the screen resolution changed.
        /// </summary>
        public event EventHandler ResolutionChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Creates the back buffer.
        /// </summary>
        /// <param name="displayMode">The display mode.</param>
        private void CreateBackBuffer(DisplayMode displayMode)
        {
            this.BackBuffer = this.TextureFactory.CreateRenderTarget(
                displayMode.Width,
                displayMode.Height);
        }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.Provider.RenderManager.Clear(color);
        }
        /// <summary>
        /// Renders to the specified render target.
        /// </summary>
        /// <param name="target">The target.</param>
        public IDisposable RenderTo(IRenderTarget target)
        {
            this.Targets.Push(target);
            return new RenderTargetDisposer(this);
        }
        /// <summary>
        /// Presents all drawn data.
        /// </summary>
        public void Present()
        {
            this.Provider.RenderManager.Present();
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            XGL.Components.Add(new ValueProvider<ITextureFactory>(this.TextureFactory));
            XGL.Components.Add(new ValueProvider<IRenderManager>(this.RenderManager));
            XGL.Components.Add(new ValueProvider<IGeometryProvider>(this.Geometry));
            XGL.Components.Add(new ValueProvider<IGeometryFactory>(this.Geometry.Factory));
            XGL.Components.Add(new ValueProvider<IGraphicsProvider>(this.Provider));

            this.CreateBackBuffer(this.DisplayMode);
        }
        #endregion
    }
}
