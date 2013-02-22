using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering.GDIPlus
{
    public class GDIGraphicsProvider : IGraphicsProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIGraphicsProvider"/> class.
        /// </summary>
        public GDIGraphicsProvider(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;

            this.TextureFactory = new GDITextureFactory();
            this.RenderManager = new GDIRenderManager(graphicsDevice);
        }
        #endregion

        #region IGraphicsProvider Member
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "GDIPlus"; }
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
        /// Gets the render provider.
        /// </summary>
        public IRenderManager RenderManager { get; private set; }
        #endregion

    }
}
