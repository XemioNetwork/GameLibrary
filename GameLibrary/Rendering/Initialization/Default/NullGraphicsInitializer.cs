namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullGraphicsInitializer : IGraphicsInitializer
    {
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Id
        {
            get { return "null"; }
        }
        #endregion

        #region Implementation of IGraphicsInitializer
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "None"; }
        }
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        public bool IsAvailable()
        {
            return true;
        }
        /// <summary>
        /// Gets or sets the smoothing mode.
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }
        /// <summary>
        /// Gets or sets the interpolation mode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public IGraphicsFactory Factory
        {
            get { return new NullGraphicsFactory(); }
        }
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
        }
        #endregion
    }
}
