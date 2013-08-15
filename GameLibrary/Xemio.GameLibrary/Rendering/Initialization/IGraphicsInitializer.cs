namespace Xemio.GameLibrary.Rendering.Initialization
{
    public interface IGraphicsInitializer
    {
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
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice);
    }
}
