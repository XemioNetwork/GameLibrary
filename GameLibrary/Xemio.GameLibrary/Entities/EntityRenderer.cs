﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Entities
{
    public abstract class EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRenderer"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected EntityRenderer(Entity entity)
        {
            this.Entity = entity;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Require<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return this.GraphicsDevice.RenderManager; }
        }
        /// <summary>
        /// Gets the geometry.
        /// </summary>
        public IGeometryManager GeometryManager
        {
            get { return this.GraphicsDevice.GeometryManager; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the specified entity.
        /// </summary>
        public virtual void Render()
        {
        }
        #endregion
    }
}
