using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using NLog;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Attributes;
using Xemio.GameLibrary.Entities.Components;
using Xemio.GameLibrary.Entities.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Entities
{
    public class Entity
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            this.Guid = Guid.NewGuid();

            this.Components = new List<EntityComponent>();
            this.Add(new PositionComponent());

            this.IsVisible = true;
        }
        #endregion

        #region Fields
        private string _name = string.Empty;
        private bool _handleComponents = true;
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this._name))
                    return this.Guid.ToString("D");

                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [ExcludeSerialization]
        public PositionComponent Position
        {
            get { return this.Get<PositionComponent>(); }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is destroyed.
        /// </summary>
        public bool IsDestroyed { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this entity is visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Gets a value indicating whether the entity has a name.
        /// </summary>
        public bool IsNamed
        {
            get { return !string.IsNullOrEmpty(this._name); }
        }
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
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Add(EntityComponent component)
        {
            component.Attach(this);
            this.Components.Add(component);
        }
        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Remove(EntityComponent component)
        {
            component.Detach();
            this.Components.Remove(component);
        }
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
        public T Get<T>() where T : EntityComponent
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
            if (this._handleComponents)
            {
                foreach (EntityComponent component in this.Components)
                {
                    component.Tick(elapsed);
                }
            }
        }
        #endregion
    }
}
