﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Entities
{
    public class EntityFactory
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFactory"/> class.
        /// </summary>
        public EntityFactory()
        {
        }
        #endregion

        #region Fields
        private int _currentID;
        #endregion
        
        #region Methods
        /// <summary>
        /// Determines whether the factory can create a new entity.
        /// </summary>
        public virtual bool CanCreate<T>() where T : Entity, new()
        {
            return true;
        }
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        public virtual T CreateLocalEntity<T>() where T : Entity, new()
        {
            return (T)this.CreateLocalEntity(typeof(T));
        }
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="type">The type.</param>
        public virtual Entity CreateLocalEntity(Type type)
        {
            return this.CreateLocalEntity(type, this.CreateID());
        }
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public virtual Entity CreateLocalEntity(Type type, int id)
        {
            Entity entity = (Entity)Activator.CreateInstance(type);
            entity.ID = id;

            return entity;
        }
        /// <summary>
        /// Creates a new ID.
        /// </summary>
        public virtual int CreateID()
        {
            return this._currentID++;
        }
        #endregion
    }
}
