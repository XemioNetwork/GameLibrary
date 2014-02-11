using System;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Rendering.Initialization
{
    [ManuallyLinked]
    public class PriorizedInitializer : IGraphicsInitializer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorizedInitializer"/> class.
        /// </summary>
        public PriorizedInitializer()
        {
            this._initializers = new List<IGraphicsInitializer>();
        }
        #endregion

        #region Fields
        private IGraphicsInitializer _current;
        private readonly List<IGraphicsInitializer> _initializers; 
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public void Add(IGraphicsInitializer provider)
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
            return this._initializers.Any(i => i.IsAvailable());
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
        public IGraphicsFactory Factory
        {
            get { return this._current.Factory; }
        }
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
            foreach (IGraphicsInitializer initializer in this._initializers)
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

            throw new InvalidOperationException("Your system doesn't support any rendering providers.");
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
