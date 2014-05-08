using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Subscribers;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Entities.Components
{
    public abstract class EntityComponent : IRenderSubscriber, ITickSubscriber
    {
        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        [Exclude]
        public Entity Entity { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AttachToEntity(Entity entity)
        {
            this.Entity = entity;
            this.Entity.Components.Add(this);

            this.OnAttached(entity);
        }
        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        public void RemoveFromEntity()
        {
            if (this.Entity != null)
            {
                Entity entity = this.Entity;

                this.Entity.Components.Remove(this);
                this.Entity = null;

                this.OnDetached(entity);
            }
        }
        /// <summary>
        /// Converts the entity to the specified type.
        /// </summary>
        protected T EntityAs<T>() where T : Entity
        {
            return this.Entity as T;
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// Called when the component got attached to an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void OnAttached(Entity entity)
        {
        }
        /// <summary>
        /// Called when the component got detached to an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void OnDetached(Entity entity)
        {
        }
        #endregion

        #region Implementation of IGameHandler
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public virtual void Render()
        {
        }
        #endregion
    }
}
