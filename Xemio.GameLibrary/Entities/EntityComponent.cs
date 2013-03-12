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

        #region Fields
        private EntityDataContainer _container;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the data container.
        /// </summary>
        protected EntityDataContainer Container
        {
            get { return this._container; }
            set
            {
                if (this._container != null)
                {
                    this.Entity.Containers.Remove(this._container);
                }

                this._container = value;
                this.Entity.Containers.Add(this._container);
            }
        }
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
