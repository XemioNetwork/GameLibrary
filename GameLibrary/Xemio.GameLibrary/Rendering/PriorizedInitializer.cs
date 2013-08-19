using System;
using System.Collections.Generic;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering
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
        private IGraphicsInitializer _currentInitializer;
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

        #region IGraphicsProvider Factory Methods
        /// <summary>
        /// Gets the texture writer.
        /// </summary>
        public IContentWriter CreateTextureWriter()
        {
            return this._currentInitializer.CreateTextureWriter();
        }
        /// <summary>
        /// Gets the texture reader.
        /// </summary>
        public IContentReader CreateTextureReader()
        {
            return this._currentInitializer.CreateTextureReader();
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager CreateRenderManager()
        {
            return this._currentInitializer.CreateRenderManager();
        }
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        public IRenderFactory CreateRenderFactory()
        {
            return this._currentInitializer.CreateRenderFactory();
        }
        /// <summary>
        /// Gets the geometry manager.
        /// </summary>
        public IGeometryManager CreateGeometryManager()
        {
            return this._currentInitializer.CreateGeometryManager();
        }
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        /// <returns></returns>
        public IGeometryFactory CreateGeometryFactory()
        {
            return this._currentInitializer.CreateGeometryFactory();
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
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
            foreach (IGraphicsInitializer provider in this._initializers)
            {
                if (provider.IsAvailable())
                {
                    this._currentInitializer = provider;

                    this._currentInitializer.SmoothingMode = this.SmoothingMode;
                    this._currentInitializer.InterpolationMode = this.InterpolationMode;

                    this._currentInitializer.Initialize(graphicsDevice);
                    break;
                }
            }

            throw new InvalidOperationException("Your system doesn't support any rendering providers.");
        }
        #endregion
    }
}
