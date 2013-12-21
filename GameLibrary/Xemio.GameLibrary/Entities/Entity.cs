using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Attributes;
using Xemio.GameLibrary.Entities.Events;
using Xemio.GameLibrary.Events;
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
            this.Guid = Guid.NewGuid();
            this.Components = new List<EntityComponent>();

            this.NotifyPositionChanged = true;
            this.IsVisible = true;
        }
        #endregion

        #region Fields
        private Vector2 _position;

        private bool _resetDirty;
        private bool _handleComponents = true;
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set
            {
                if (value != this._position)
                {
                    Vector2 delta = value - this._position;

                    this._resetDirty = false;
                    this.IsDirty = true;

                    this._position = value;

                    if (this.NotifyPositionChanged)
                    {
                        this.OnPositionChanged(delta);
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether to publish an event if the position changed.
        /// </summary>
        public bool NotifyPositionChanged { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is destroyed.
        /// </summary>
        public bool IsDestroyed { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is dirty.
        /// </summary>
        public bool IsDirty { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this entity is visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<EntityComponent> Components { get; set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        [ExcludeSerialization]
        public EntityEnvironment Environment { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Enables the component tick.
        /// </summary>
        public void EnableComponents()
        {
            this._handleComponents = true;
        }
        /// <summary>
        /// Disables the component tick.
        /// </summary>
        public void DisableComponents()
        {
            this._handleComponents = false;
        }
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
        /// Called when the position changed.
        /// </summary>
        /// <param name="delta">The delta.</param>
        public virtual void OnPositionChanged(Vector2 delta)
        {
            XGL.Components.Get<EventManager>().Publish(new EntityPositionChangedEvent(this, delta));
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public virtual void Initialize(EntityEnvironment environment)
        {
        }
        /// <summary>
        /// Renders this entity.
        /// </summary>
        public virtual void Render()
        {
            if (this._handleComponents && this.IsVisible)
            {
                foreach (EntityComponent component in this.Components)
                {
                    component.Render();
                }
            }
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

            if (this._handleComponents)
            {
                foreach (EntityComponent component in this.Components)
                {
                    component.Tick(elapsed);
                }
            }

            //Make the IsDirty property accessible after the entity tick.
            this._resetDirty = true;
        }
        #endregion
    }
}
