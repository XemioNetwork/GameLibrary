using System;
using System.Collections.Generic;

namespace Xemio.GameLibrary.Rendering.Initialization
{
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
        private readonly List<IGraphicsInitializer> _initializers; 
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds the specified initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public void Add(IGraphicsInitializer initializer)
        {
            this._initializers.Add(initializer);
        }
        #endregion

        #region IGraphicsInitializer Member
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
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice)
        {
            foreach (IGraphicsInitializer initializer in this._initializers)
            {
                if (initializer.IsAvailable())
                {
                    initializer.SmoothingMode = this.SmoothingMode;
                    initializer.InterpolationMode = this.InterpolationMode;

                    return initializer.CreateProvider(graphicsDevice);
                }
            }

            throw new InvalidOperationException("Your system doesn't support any rendering providers.");
        }
        #endregion
    }
}
