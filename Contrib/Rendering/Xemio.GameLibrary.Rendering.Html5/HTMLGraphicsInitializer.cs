using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.HTML5.Geometry;
using Xemio.GameLibrary.Rendering.HTML5.Serialization;

namespace Xemio.GameLibrary.Rendering.HTML5
{
    public class HTMLGraphicsInitializer : IGraphicsInitializer
    {
        #region Implementation of IGraphicsInitializer
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        public bool IsAvailable()
        {
            return true;
        }
        /// <summary>
        /// Creates the texture writer.
        /// </summary>
        /// <returns></returns>
        public IContentWriter CreateTextureWriter()
        {
            return new HTMLTextureWriter();
        }
        /// <summary>
        /// Creates the texture reader.
        /// </summary>
        /// <returns></returns>
        public IContentReader CreateTextureReader()
        {
            return new HTMLTextureReader();
        }
        /// <summary>
        /// Creates the render manager.
        /// </summary>
        /// <returns></returns>
        public IRenderManager CreateRenderManager()
        {
            return new HTMLRenderManager();
        }
        /// <summary>
        /// Creates the render factory.
        /// </summary>
        /// <returns></returns>
        public IRenderFactory CreateRenderFactory()
        {
            return new HTMLRenderFactory();
        }
        /// <summary>
        /// Creates the geometry manager.
        /// </summary>
        /// <returns></returns>
        public IGeometryManager CreateGeometryManager()
        {
            return new HTMLGeometryManager();
        }
        /// <summary>
        /// Creates the geometry factory.
        /// </summary>
        /// <returns></returns>
        public IGeometryFactory CreateGeometryFactory()
        {
            return new HTMLGeometryFactory();
        }
        /// <summary>
        /// Initializes the specified graphics device.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
        }
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "HTML5 Canvas"; }
        }
        /// <summary>
        /// Gets or sets the smoothing mode.
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }
        /// <summary>
        /// Gets or sets the interpolation mode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }
        #endregion
    }
}
