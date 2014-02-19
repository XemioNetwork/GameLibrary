using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using NLog;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Handlers;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Entities
{
    public class EntityEnvironment : IEnumerable<Entity>, ITickHandler, IRenderHandler
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
            this.Entities = new CachedList<Entity>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Guid, Entity> _guidMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity count.
        /// </summary>
        public int Count
        {
            get { return this.Entities.Count; }
        }
        /// <summary>
        /// Gets the entities.
        /// </summary>
        protected CachedList<Entity> Entities { get; private set; }
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
            logger.Trace("Added entity {0} to entity environment.", entity.GetType().Name);

            entity.Environment = this;
            entity.Initialize(this);

            this._guidMappings.Add(entity.Guid, entity);
            this.Entities.Add(entity);
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Remove(Entity entity)
        {
            logger.Trace("Removed entity {0} from entity environment.", entity.GetType().Name);

            entity.Environment = null;

            this._guidMappings.Remove(entity.Guid);
            this.Entities.Remove(entity);
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            logger.Debug("Clearing {0} entities from entity environment.", this.Entities.Count);

            using (this.Entities.StartCaching())
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
        protected virtual IEnumerable<Entity> AsSortedEntityCollection()
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
            using (this.Entities.StartCaching())
            { 
                foreach (Entity entity in this.Entities)
                {
                    entity.Tick(elapsed);
                    if (entity.IsDestroyed)
                    {
                        this.Remove(entity);
                    }
                }
            }
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public virtual void Render()
        {
            var entities = this.AsSortedEntityCollection();

            using (this.Entities.StartCaching())
            { 
                foreach (Entity entity in entities)
                {
                    entity.Render();
                }
            }
        }
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Entity> GetEnumerator()
        {
            return this.AsSortedEntityCollection().GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
