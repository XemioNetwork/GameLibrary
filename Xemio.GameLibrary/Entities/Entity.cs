using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Common;

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
            this.ID = -1;

            this.Components = new List<EntityComponent>();
            this.Containers = new List<EntityDataContainer>();

            this.HandleComponentTick = true;
        }
        #endregion

        #region Fields
        private Vector2 _position;
        private bool _resetDirty;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set
            {
                this._position = value;

                this._resetDirty = false;
                this.IsDirty = true;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is destroyed.
        /// </summary>
        public bool IsDestroyed { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is synced.
        /// </summary>
        public bool IsSynced { get; protected set; }
        /// <summary>
        /// Gets a value indicating whether this instance is dirty.
        /// </summary>
        public bool IsDirty { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is tick enabled.
        /// </summary>
        public bool HandleComponentTick { get; set; }
        /// <summary>
        /// Gets the components.
        /// </summary>
        [ExcludeSync]
        public List<EntityComponent> Components { get; private set; }
        /// <summary>
        /// Gets the data containers.
        /// </summary>
        [ExcludeSync]
        public List<EntityDataContainer> Containers { get; private set; }
        /// <summary>
        /// Gets the renderer.
        /// </summary>
        [ExcludeSync]
        public EntityRenderer Renderer { get; protected set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        [ExcludeSync]
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
        /// Gets a container by the specified ID.
        /// </summary>
        /// <param name="id">The id.</param>
        public EntityDataContainer GetContainer(int id)
        {
            return this.Containers.FirstOrDefault(container => container.ID == id);
        }
        /// <summary>
        /// Gets a specific container by a specified type.
        /// </summary>
        public T GetContainer<T>() where T : EntityDataContainer
        {
            return this.Containers.FirstOrDefault(container => container is T) as T;
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
            if (this._resetDirty)
            {
                this.IsDirty = false;
            }

            if (this.HandleComponentTick)
            {
                foreach (EntityComponent component in this.Components)
                {
                    component.Tick(elapsed);
                }
            }

            this._resetDirty = true;
        }
        #endregion
    }
}
