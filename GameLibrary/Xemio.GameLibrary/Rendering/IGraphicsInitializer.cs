using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering
{
    public interface IGraphicsInitializer
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        bool IsAvailable();
        /// <summary>
        /// Gets or sets the smoothing mode.
        /// </summary>
        SmoothingMode SmoothingMode { get; set; }
        /// <summary>
        /// Gets or sets the interpolation mode.
        /// </summary>
        InterpolationMode InterpolationMode { get; set; }
        /// <summary>
        /// Gets the texture writer.
        /// </summary>
        IContentWriter CreateTextureWriter();
        /// <summary>
        /// Gets the texture reader.
        /// </summary>
        IContentReader CreateTextureReader();
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        IRenderManager CreateRenderManager();
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        IRenderFactory CreateRenderFactory();
        /// <summary>
        /// Gets the geometry manager.
        /// </summary>
        IGeometryManager CreateGeometryManager();
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        /// <returns></returns>
        IGeometryFactory CreateGeometryFactory();
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        void Initialize(GraphicsDevice graphicsDevice);
    }
}
