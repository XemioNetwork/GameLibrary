﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        #region IGraphicsProvider GuidFactory Methods
        /// <summary>
        /// Gets the texture writer.
        /// </summary>
        public IWriter CreateTextureWriter()
        {
            return this._current.CreateTextureWriter();
        }
        /// <summary>
        /// Gets the texture reader.
        /// </summary>
        public IReader CreateTextureReader()
        {
            return this._current.CreateTextureReader();
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager CreateRenderManager()
        {
            return this._current.CreateRenderManager();
        }
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        public IRenderFactory CreateRenderFactory()
        {
            return this._current.CreateRenderFactory();
        }
        /// <summary>
        /// Gets the geometry manager.
        /// </summary>
        public IGeometryManager CreateGeometryManager()
        {
            return this._current.CreateGeometryManager();
        }
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        /// <returns></returns>
        public IGeometryFactory CreateGeometryFactory()
        {
            return this._current.CreateGeometryFactory();
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
    }
}
