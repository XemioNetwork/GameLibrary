using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Entities
{
    public class EntityEnvironment : IEnumerable<Entity>, IGameHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEnvironment"/> class.
        /// </summary>
        public EntityEnvironment()
        {
            this._idMappings = new Dictionary<int, Entity>();

            this.Factory = new EntityIdFactory();
            this.Entities = new CachedList<Entity>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<int, Entity> _idMappings;
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
        /// Gets or sets the factory.
        /// </summary>
        public EntityIdFactory Factory { get; set; }
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
        /// <param name="id">The id.</param>
        public Entity GetEntity(int id)
        {
            if (this._idMappings.ContainsKey(id))
            {
                return this._idMappings[id];
            }

            return null;
        }
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(Entity entity)
        {
            if (entity.EntityId < 0)
            {
                entity.EntityId = this.Factory.CreateId();
            }

            entity.Environment = this;
            entity.Initialize(this);

            this._idMappings.Add(entity.EntityId, entity);
            this.Entities.Add(entity);
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Remove(Entity entity)
        {
            entity.EntityId = -1;
            entity.Environment = null;

            this._idMappings.Remove(entity.EntityId);
            this.Entities.Remove(entity);
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
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
        protected virtual IEnumerable<Entity> SortedEntityCollection()
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
            var entities = this.SortedEntityCollection();

            using (this.Entities.StartCaching())
            { 
                foreach (Entity entity in entities)
                {
                    if (entity.Renderer != null)
                    {
                        entity.Renderer.Render();
                    }
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
            return this.SortedEntityCollection().GetEnumerator();
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
