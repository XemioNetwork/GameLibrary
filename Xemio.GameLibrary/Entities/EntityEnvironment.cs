using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Xemio.GameLibrary.Game;

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
            this._addCache = new Queue<Entity>();
            this._removeCache = new Queue<Entity>();

            this._idMappings = new Dictionary<int, Entity>();

            this.Factory = new EntityFactory();
            this.Entities = new List<Entity>();
        }
        #endregion

        #region Fields
        private Queue<Entity> _addCache;
        private Queue<Entity> _removeCache;

        private Dictionary<int, Entity> _idMappings;

        private bool _enumerating;
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
        protected List<Entity> Entities { get; private set; }
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        public EntityFactory Factory { get; set; }
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
        /// Applies all added or removed entities.
        /// </summary>
        private void ApplyCachedChanges()
        {
            foreach (Entity entity in this._addCache)
            {
                this.Add(entity);
            }
            foreach (Entity entity in this._removeCache)
            {
                this.Remove(entity);
            }

            this._addCache.Clear();
            this._removeCache.Clear();
        }
        /// <summary>
        /// Begins the enumeration.
        /// </summary>
        protected void BeginEnumeration()
        {
            this._enumerating = true;
        }
        /// <summary>
        /// Ends the enumeration.
        /// </summary>
        protected void EndEnumeration()
        {
            this._enumerating = false;
            this.ApplyCachedChanges();
        }
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
        protected void AddMapped(Entity entity)
        {
            entity.Environment = this;

            this._idMappings.Add(entity.ID, entity);
            this.Entities.Add(entity);
        }
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(Entity entity)
        {
            if (this._enumerating)
            {
                this._addCache.Enqueue(entity);
                return;
            }
            if (entity.ID < 0)
            {
                entity.ID = this.Factory.CreateID();
            }

            this.AddMapped(entity);
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected void RemoveMapped(Entity entity)
        {
            entity.ID = -1;
            entity.Environment = null;

            this._idMappings.Remove(entity.ID);
            this.Entities.Remove(entity);
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Remove(Entity entity)
        {
            if (this._enumerating)
            {
                this._removeCache.Enqueue(entity);
                return;
            }

            this.RemoveMapped(entity);
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.BeginEnumeration();
            foreach (Entity entity in this.Entities)
            {
                this.Remove(entity);
            }

            this.EndEnumeration();
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
            this.BeginEnumeration();
            foreach (Entity entity in this.Entities)
            {
                entity.Tick(elapsed);
                if (entity.IsDestroyed)
                {
                    this.Remove(entity);
                }
            }

            this.EndEnumeration();
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public virtual void Render()
        {
            IEnumerable<Entity> entities = this.SortedEntityCollection();

            this.BeginEnumeration();
            foreach (Entity entity in entities)
            {
                if (entity.Renderer != null)
                {
                    entity.Renderer.Render();
                }
            }

            this.EndEnumeration();
        }
        #endregion

        #region IEnumerable<Entity> Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Entity> GetEnumerator()
        {
            return this.SortedEntityCollection().GetEnumerator();
        }
        #endregion

        #region IEnumerable Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
