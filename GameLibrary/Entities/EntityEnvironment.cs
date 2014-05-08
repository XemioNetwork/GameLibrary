using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Events.Handles;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Subscribers;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Logging;

namespace Xemio.GameLibrary.Entities
{
    public class EntityEnvironment : IEnumerable<Entity>, ITickSubscriber, IRenderSubscriber, IHandleContainer
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEnvironment"/> class.
        /// </summary>
        public EntityEnvironment()
        {
            this._guidMappings = new Dictionary<Guid, Entity>();

            this.Systems = new AutoProtectedList<EntitySystem>();
            this.Entities = new AutoProtectedList<Entity>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Guid, Entity> _guidMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the systems.
        /// </summary>
        protected ProtectedList<EntitySystem> Systems { get; private set; } 
        /// <summary>
        /// Gets the entities.
        /// </summary>
        protected ProtectedList<Entity> Entities { get; private set; }
        /// <summary>
        /// Gets the entity count.
        /// </summary>
        public int Count
        {
            get { return this.Entities.Count; }
        }
        /// <summary>
        /// Gets the <see cref="Xemio.GameLibrary.Entities.Entity"/> at the specified index.
        /// </summary>
        public Entity this[int index]
        {
            get { return this.Entities[index]; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets an entity by a specified ID.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public Entity GetEntity(Guid guid)
        {
            if (this._guidMappings.ContainsKey(guid))
            {
                return this._guidMappings[guid];
            }

            return null;
        }
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(Entity entity)
        {
            logger.Trace("Added entity {0} to entity environment.", entity);
            entity.Initialize(this);

            this._guidMappings[entity.Guid] = entity;
            this.Entities.Add(entity);
        }
        /// <summary>
        /// Adds the specified system.
        /// </summary>
        /// <param name="system">The system.</param>
        public virtual void Add(EntitySystem system)
        {
            logger.Trace("Added systen {0} to entity environment.", system);
            system.Initialize(this);

            this.Systems.Add(system);
        }
        /// <summary>
        /// Removes the entity identified by the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public void Remove(Guid guid)
        {
            this.Remove(this.GetEntity(guid));
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Remove(Entity entity)
        {
            logger.Trace("Removed entity {0} from entity environment.", entity);

            this._guidMappings.Remove(entity.Guid);
            this.Entities.Remove(entity);
        }
        /// <summary>
        /// Removes the specified system.
        /// </summary>
        /// <param name="system">The system.</param>
        public virtual void Remove(EntitySystem system)
        {
            logger.Trace("Removed system {0} from entity environment.", system);

            this.Systems.Remove(system);
        }
        /// <summary>
        /// Creates a new entity system of the specified type.
        /// </summary>
        /// <typeparam name="T">The system type.</typeparam>
        public void CreateSystem<T>() where T : EntitySystem, new()
        {
            this.Add(new T());
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            logger.Debug("Clearing {0} entities from entity environment.", this.Entities.Count);

            using (this.Entities.Protect())
            { 
                foreach (Entity entity in this.Entities)
                {
                    this.Remove(entity);
                }
            }
        }
        /// <summary>
        /// Sorts the entities.
        /// </summary>
        protected virtual IEnumerable<Entity> SortEntities()
        {
            return this.Entities;
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            foreach (Entity entity in this.Entities)
            {
                entity.Tick(elapsed);
                if (entity.IsDestroyed)
                {
                    this.Remove(entity);
                }
            }

            foreach (EntitySystem system in this.Systems)
            {
                system.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public virtual void Render()
        {
            foreach (EntitySystem system in this.Systems) 
                system.PreRender();

            foreach (Entity entity in this.SortEntities())
                entity.Render();

            foreach (EntitySystem system in this.Systems)
                system.PostRender();
        }
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Entity> GetEnumerator()
        {
            return this.SortEntities().GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Implementation of IHandleContainer
        /// <summary>
        /// Gets an instance list containing handle implementations.
        /// </summary>
        IEnumerable IHandleContainer.Children
        {
            get { return this.Entities; }
        }
        #endregion
    }
}
