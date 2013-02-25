using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Entities
{
    public class Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            this.Components = new List<EntityComponent>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is destroyed.
        /// </summary>
        public bool IsDestroyed { get; private set; }
        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<EntityComponent> Components { get; private set; }
        /// <summary>
        /// Gets the renderer.
        /// </summary>
        public EntityRenderer Renderer { get; protected set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a specific component by a specified type.
        /// </summary>
        public T GetComponent<T>() where T : EntityComponent
        {
            return this.Components.FirstOrDefault(component => component is T) as T;
        }
        /// <summary>
        /// Destroys this entity.
        /// </summary>
        public virtual void Destroy()
        {
            this.IsDestroyed = true;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            foreach (EntityComponent component in this.Components)
            {
                component.Tick(elapsed);
            }
        }
        #endregion
    }
}
