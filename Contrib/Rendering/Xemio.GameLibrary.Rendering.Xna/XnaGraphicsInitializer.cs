using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.Xna.Serialization;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaGraphicsInitializer : IGraphicsInitializer
    {
        #region Implementation of IGraphicsInitializer
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        public bool IsAvailable()
        {
            return XnaHelper.TryCreateDevice();
        }
        /// <summary>
        /// Creates the texture writer.
        /// </summary>
        /// <returns></returns>
        public IContentWriter CreateTextureWriter()
        {
            return new XnaTextureWriter();
        }
        /// <summary>
        /// Creates the texture reader.
        /// </summary>
        /// <returns></returns>
        public IContentReader CreateTextureReader()
        {
            return new XnaTextureReader();
        }
        /// <summary>
        /// Creates the render manager.
        /// </summary>
        /// <returns></returns>
        public IRenderManager CreateRenderManager()
        {
            return new XnaRenderManager(this.InterpolationMode);
        }
        /// <summary>
        /// Creates the render factory.
        /// </summary>
        /// <returns></returns>
        public IRenderFactory CreateRenderFactory()
        {
            return new XnaRenderFactory();
        }
        /// <summary>
        /// Creates the geometry manager.
        /// </summary>
        /// <returns></returns>
        public IGeometryManager CreateGeometryManager()
        {
            return null;
        }
        /// <summary>
        /// Creates the geometry factory.
        /// </summary>
        /// <returns></returns>
        public IGeometryFactory CreateGeometryFactory()
        {
            return null;
        }
        /// <summary>
        /// Initializes the specified graphics device.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
            XnaHelper.CreateDevice(graphicsDevice);
        }
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "XNA Framework 4.0"; }
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
