using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Entities
{
    public abstract class EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityComponent(Entity entity)
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
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        #endregion
    }
}
