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

            this.Entities = new List<Entity>();
        }
        #endregion

        #region Fields
        private Queue<Entity> _addCache;
        private Queue<Entity> _removeCache;

        private bool _enumerating;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entities.
        /// </summary>
        internal List<Entity> Entities { get; private set; }
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
        private void BeginEnumeration()
        {
            this._enumerating = true;
        }
        /// <summary>
        /// Ends the enumeration.
        /// </summary>
        private void EndEnumeration()
        {
            this._enumerating = false;
            this.ApplyCachedChanges();
        }
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(Entity entity)
        {
            if (this._enumerating)
            {
                this._addCache.Enqueue(entity);
                return;
            }

            entity.Environment = this;
            this.Entities.Add(entity);
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(Entity entity)
        {
            if (this._enumerating)
            {
                this._removeCache.Enqueue(entity);
                return;
            }

            entity.Environment = null;
            this.Entities.Remove(entity);
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
            this.BeginEnumeration();
            foreach (Entity entity in this.Entities)
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
            return this.Entities.GetEnumerator();
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
