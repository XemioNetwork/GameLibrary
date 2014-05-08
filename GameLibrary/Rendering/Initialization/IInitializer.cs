using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Rendering.Initialization
{
    public interface IInitializer : ILinkable<string>
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
        /// Gets the factory.
        /// </summary>
        IInitializationFactory Factory { get; }
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        void Initialize(GraphicsDevice graphicsDevice);
    }
}
