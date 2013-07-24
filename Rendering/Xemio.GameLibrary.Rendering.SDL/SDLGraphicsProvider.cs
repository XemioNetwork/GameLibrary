using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SdlDotNet.Graphics;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.Rendering.SDL
{
    public class SDLGraphicsProvider : IGraphicsProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SDLGraphicsProvider"/> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public SDLGraphicsProvider(GraphicsDevice graphicsDevice)
        {
            Video.Initialize();

            this.TextureFactory = new SDLTextureFactory();
            this.RenderManager = new SDLRenderManager(graphicsDevice);
        }
        #endregion
        
        #region Implementation of IGraphicsProvider
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "Simple DirectMedia Layer"; }
        }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory { get; private set; }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager { get; private set; }
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
        public IGeometryProvider Geometry { get; private set; }
        #endregion
    }
}
