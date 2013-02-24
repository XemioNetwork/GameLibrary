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
    public class GraphicsDevice : IComponent, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsDevice"/> class.
        /// </summary>
        public GraphicsDevice(IntPtr handle)
        {
            this.Handle = handle;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public IGraphicsProvider Graphics { get; set; }
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
                if (this.Graphics.IsGeometrySupported)
                {
                    return this.Graphics.Geometry;
                }

                return GeometryProvider.Empty;
            }
        }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory
        {
            get { return this.Graphics.TextureFactory; }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return this.Graphics.RenderManager; }
        }
        /// <summary>
        /// Gets the display mode.
        /// </summary>
        public DisplayMode DisplayMode { get; private set; }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the screen resolution changed.
        /// </summary>
        public event EventHandler ResolutionChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.Graphics.RenderManager.Clear(color);
        }
        /// <summary>
        /// Presents all drawn data.
        /// </summary>
        public void Present()
        {
            this.Graphics.RenderManager.Present();
        }
        /// <summary>
        /// Sets the display mode.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void SetDisplayMode(int width, int height)
        {
            this.DisplayMode = new DisplayMode(width, height);
            if (this.ResolutionChanged != null)
            {
                this.ResolutionChanged(this, EventArgs.Empty);
            }
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            ComponentManager.Instance.Add(new ValueProvider<ITextureFactory>(this.TextureFactory));
            ComponentManager.Instance.Add(new ValueProvider<IRenderManager>(this.RenderManager));

            ComponentManager.Instance.Add(new ValueProvider<IGraphicsProvider>(this.Graphics));
        }
        #endregion
    }
}
