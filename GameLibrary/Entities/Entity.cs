using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using NLog;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Entities.Components;
using Xemio.GameLibrary.Entities.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Handlers;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Entities
{
    public class Entity : ITickHandler, IRenderHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            this.Guid = Guid.NewGuid();

            this.Components = new List<EntityComponent>();
            this.AddComponent(new TransformComponent());

            this.IsVisible = true;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Guid { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is destroyed.
        /// </summary>
        public bool IsDestroyed { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this entity is visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<EntityComponent> Components { get; set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public TransformComponent Transform
        {
            get { return this.GetComponent<TransformComponent>(); }
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void AddComponent(EntityComponent component)
        {
            component.AttachToEntity(this);
        }
        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void RemoveComponent(EntityComponent component)
        {
            component.RemoveFromEntity();
        }
        /// <summary>
        /// Gets the component of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public EntityComponent GetComponent(Type type)
        {
            return this.Components.FirstOrDefault(type.IsInstanceOfType);
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
        /// Called, when the entity was attached to an entity environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public virtual void Initialize(EntityEnvironment environment)
        {
            this.Environment = environment;
        }
        /// <summary>
        /// Renders this entity.
        /// </summary>
        public void Render()
        {
            if (this.IsEnabled && this.IsVisible)
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
        public void Tick(float elapsed)
        {
            if (this.IsEnabled)
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
