using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Entities
{
    public class EntityFactory
    {
        #region Fields
        private int _currentId;
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static EntityFactory Instance
        {
            get { return Singleton<EntityFactory>.Value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        public T CreateEntity<T>() where T : Entity, new()
        {
            return (T)this.CreateEntity(typeof(T));
        }
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="type">The type.</param>
        public Entity CreateEntity(Type type)
        {
            return this.CreateEntity(type, this.CreateId());
        }
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public Entity CreateEntity(Type type, int id)
        {
            Entity entity = (Entity)Activator.CreateInstance(type);
            entity.EntityId = id;

            return entity;
        }
        /// <summary>
        /// Creates a new Id.
        /// </summary>
        public int CreateId()
        {
            return this._currentId++;
        }
        #endregion
    }
}
