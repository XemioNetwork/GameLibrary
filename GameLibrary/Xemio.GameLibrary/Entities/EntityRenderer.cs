using System;
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
        /// Gets the <see cref="Entity"/>.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the <see cref="GraphicsDevice"/>.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Require<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the <see cref="RenderManager"/>.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return this.GraphicsDevice.RenderManager; }
        }
        /// <summary>
        /// Gets the <see cref="RenderFactory"/>.
        /// </summary>
        public IRenderFactory RenderFactory
        {
            get { return this.GraphicsDevice.RenderFactory; }
        }
        /// <summary>
        /// Gets the <see cref="GeometryManager"/>.
        /// </summary>
        public IGeometryManager GeometryManager
        {
            get { return this.GraphicsDevice.GeometryManager; }
        }
        /// <summary>
        /// Gets the <see cref="GeometryFactory"/>.
        /// </summary>
        public IGeometryFactory GeometryFactory
        {
            get { return this.GraphicsDevice.GeometryFactory; }
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
