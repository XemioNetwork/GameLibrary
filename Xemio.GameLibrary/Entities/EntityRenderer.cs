using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Entities
{
    public abstract class EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRenderer"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityRenderer(Entity entity)
        {
            this.Entity = entity;
            this.RenderManager = ComponentManager.Get<IRenderManager>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager { get; private set; }
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
