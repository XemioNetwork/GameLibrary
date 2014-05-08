using System;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Rendering.Initialization.Default;

namespace Xemio.GameLibrary.Rendering.Initialization
{
    [ManuallyLinked]
    public class PriorizedInitializer : IInitializer
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorizedInitializer"/> class.
        /// </summary>
        public PriorizedInitializer()
        {
            this._initializers = new List<IInitializer>();
        }
        #endregion

        #region Fields
        private IInitializer _current = new NullInitializer();
        private readonly List<IInitializer> _initializers; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to exit if none of the specified initializers is available.
        /// </summary>
        public bool FailIfNoneAvailable { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public void Add(IInitializer provider)
        {
            this._initializers.Add(provider);
        }
        #endregion
        
        #region IGraphicsProvider Member
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        /// <returns></returns>
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
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public IInitializationFactory Factory
        {
            get { return this._current.Factory; }
        }
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
            foreach (IInitializer initializer in this._initializers)
            {
                if (initializer.IsAvailable())
                {
                    this._current = initializer;

                    this._current.SmoothingMode = this.SmoothingMode;
                    this._current.InterpolationMode = this.InterpolationMode;

                    this._current.Initialize(graphicsDevice);
                    break;
                }
            }

            if (this.FailIfNoneAvailable)
            {
                throw new InvalidOperationException("Your system doesn't support any of the specified rendering providers.");
            }

            logger.Warn("Your system doesn't support any of the specified rendering providers, initializing with NullInitializer as fallback.");
        }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Id
        {
            get { return "priorized"; }
        }
        #endregion
    }
}
