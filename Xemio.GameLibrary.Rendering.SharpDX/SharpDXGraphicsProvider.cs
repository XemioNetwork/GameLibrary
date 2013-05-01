using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.SharpDX.Geometry;

namespace Xemio.GameLibrary.Rendering.SharpDX
{
    internal class SharpDXGraphicsProvider : IGraphicsProvider
    {
        #region Constructor
        /// <summary>
        /// Create new SharpDXGraphicsProvider
        /// </summary>
        /// <param name="device"></param>
        public SharpDXGraphicsProvider(GraphicsDevice device)
        {
            this.GraphicsDevice = device;
            this.TextureFactory = new SharpDXTextureFactory();

            SharpDXRenderManager renderManager = new SharpDXRenderManager(this);

            this.RenderManager = renderManager;
            this.Geometry = new SharpDXGeometryProvider(renderManager);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "SharpDX - DirectX 10.0"; }
        }
        /// <summary>
        /// Gets the geometry.
        /// </summary>
        public IGeometryProvider Geometry { get; private set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this graphics provider supports geometry.
        /// </summary>
        public bool IsGeometrySupported
        {
            get { return true; }
        }
        /// <summary>
        /// Gets the render provider.
        /// </summary>
        public IRenderManager RenderManager { get; private set; }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory { get; private set; }
        #endregion
    }
}
