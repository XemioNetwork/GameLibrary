using System;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.Initialization
{
    [ManuallyLinked]
    public class NamedInitializer : IGraphicsInitializer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedInitializer"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public NamedInitializer(string id)
        {
            this.Id = id;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the initializer.
        /// </summary>
        public IGraphicsInitializer Initializer
        {
            get
            {
                var implementations = XGL.Components.Require<IImplementationManager>();
                var initializer = implementations.Get<string, IGraphicsInitializer>(this.Id);

                if (initializer == null)
                {
                    throw new InvalidOperationException("Graphics initializer " + this.Id + " does not exist. Make sure you installed a graphics library for the selected initializer.");
                }

                return initializer;
            }
        }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Id { get; private set; }
        #endregion

        #region Implementation of IGraphicsInitializer
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return this.Initializer.DisplayName; }
        }
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        public bool IsAvailable()
        {
            return this.Initializer.IsAvailable();
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
            get { return this.Initializer.Factory; }
        }
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
            this.Initializer.SmoothingMode = this.SmoothingMode;
            this.Initializer.InterpolationMode = this.InterpolationMode;

            this.Initializer.Initialize(graphicsDevice);
        }
        #endregion
    }
}
