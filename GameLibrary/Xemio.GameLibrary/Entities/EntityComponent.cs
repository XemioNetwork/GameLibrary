using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Entities
{
    public abstract class EntityComponent : IGameHandler
    {
        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the entity.
        /// </summary>
        protected T GetEntity<T>() where T : Entity
        {
            return this.Entity as T;
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
