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
            this.EntityId = -1;

            this.Components = new List<EntityComponent>();
            this.Containers = new List<EntityDataContainer>();
        }
        #endregion

        #region Fields
        private Vector2 _position;

        private bool _resetDirty;
        private bool _handleComponentTick = true;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int EntityId { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set
            {
                this.OnPositionChanged(value - this._position);
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
        /// Gets or sets a value indicating whether this instance is creation synced.
        /// </summary>
        public bool IsCreationSynced { get; protected set; }
        /// <summary>
        /// Gets a value indicating whether this instance is dirty.
        /// </summary>
        public bool IsDirty { get; set; }
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
        /// Enables the component tick.
        /// </summary>
        public void EnableComponents()
        {
            this._handleComponentTick = true;
        }
        /// <summary>
        /// Disables the component tick.
        /// </summary>
        public void DisableComponents()
        {
            this._handleComponentTick = false;
        }
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
            return this.Containers.FirstOrDefault(container => container.Id == id);
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
        /// Called when the position changed.
        /// </summary>
        /// <param name="delta">The delta.</param>
        public virtual void OnPositionChanged(Vector2 delta)
        {
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

            if (this._handleComponentTick)
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
