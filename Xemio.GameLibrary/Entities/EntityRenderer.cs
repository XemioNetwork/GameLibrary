using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        #endregion

        #region Methods
        public virtual void Render()
        {

        }
        #endregion
    }
}
