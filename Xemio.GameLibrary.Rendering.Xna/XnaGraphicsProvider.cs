using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaGraphicsProvider : IGraphicsProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaGraphicsProvider"/> class.
        /// </summary>
        public XnaGraphicsProvider(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;

            this.TextureFactory = new XnaTextureFactory();
            this.RenderManager = new XnaRenderManager(this, graphicsDevice);
        }
        #endregion

        #region IGraphicsProvider Member
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this graphics provider supports geometry.
        /// </summary>
        public bool IsGeometrySupported
        {
            get { return false; }
        }
        /// <summary>
        /// Gets the geometry.
        /// </summary>
        public IGeometryProvider Geometry
        {
            get { throw new NotImplementedException(); }
        }
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "Microsoft XNA 4.0"; }
        }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory { get; private set; }
        /// <summary>
        /// Gets the render provider.
        /// </summary>
        public IRenderManager RenderManager { get; private set; }
        #endregion
    }
}
