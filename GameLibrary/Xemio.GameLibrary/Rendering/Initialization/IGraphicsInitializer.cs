namespace Xemio.GameLibrary.Rendering.Initialization
{
    public interface IGraphicsInitializer
    {
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        bool IsAvailable();
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice);
    }
}
