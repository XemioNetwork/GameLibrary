using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Entities.Components;
using Xemio.GameLibrary.Game.Handlers;

namespace Xemio.GameLibrary.Entities
{
    public abstract class EntitySystem : ITickHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySystem"/> class.
        /// </summary>
        protected EntitySystem()
        {
            this._componentFilters = new List<Type>();
            this._typeFilters = new List<Type>();
        }
        #endregion

        #region Fields
        private readonly List<Type> _componentFilters;
        private readonly List<Type> _typeFilters;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Determines wether the specified entity is matched by the filters.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected bool Matches(Entity entity)
        {
            return this._typeFilters.All(type => type.IsInstanceOfType(entity)) &&
                   this._componentFilters.All(component => entity.GetComponent(component) != null);
        }
        /// <summary>
        /// Filters the entities.
        /// </summary>
        protected IEnumerable<Entity> GetEntities()
        {
            return this.GetEntities(this.Environment);
        } 
        /// <summary>
        /// Filters the entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        protected IEnumerable<Entity> GetEntities(IEnumerable<Entity> entities)
        {
            return entities.Where(this.Matches);
        } 
        #endregion

        #region Filter Methods
        /// <summary>
        /// Requires the entities to contain the specified component.
        /// </summary>
        /// <param name="type">The type.</param>
        protected void FilterByComponent(Type type)
        {
            this._componentFilters.Add(type);
        }
        /// <summary>
        /// Requires the entities to contain the specified component.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        protected void FilterByComponent<T>() where T : EntityComponent
        {
            this._componentFilters.Add(typeof(T));
        }
        /// <summary>
        /// Requires the entities to be an instance of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        protected void FilterByEntityType(Type type)
        {
            this._componentFilters.Add(type);
        }
        /// <summary>
        /// Requires the entities to be an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        protected void FilterByEntityType<T>()
        {
            this._typeFilters.Add(typeof(T));
        }
        #endregion

        #region GameLoop Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed time since last tick.</param>
        public void Tick(float elapsed)
        {
            this.Tick(this.GetEntities(), elapsed);
        }
        /// <summary>
        /// Handles pre render calls.
        /// </summary>
        public void PreRender()
        {
            this.PreRender(this.GetEntities());
        }
        /// <summary>
        /// Handles post render calls.
        /// </summary>
        public void PostRender()
        {
            this.PostRender(this.GetEntities());
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Initializes the system inside the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public virtual void Initialize(EntityEnvironment environment)
        {
            this.Environment = environment;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="elapsed">The elapsed.</param>
        protected virtual void Tick(IEnumerable<Entity> entities, float elapsed)
        {
        }
        /// <summary>
        /// Renders the specified entities before they got rendered.
        /// </summary>
        /// <param name="entities">The entities.</param>
        protected virtual void PreRender(IEnumerable<Entity> entities)
        {
        }
        /// <summary>
        /// Renders the specified entities after they got rendered.
        /// </summary>
        /// <param name="entities">The entities.</param>
        protected virtual void PostRender(IEnumerable<Entity> entities)
        {
        }
        #endregion
    }
}
