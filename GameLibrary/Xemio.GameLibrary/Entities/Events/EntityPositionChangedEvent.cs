using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Events
{
    public class EntityPositionChangedEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPositionChangedEvent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="delta">The delta.</param>
        public EntityPositionChangedEvent(Entity entity, Vector2 delta)
        {
            this.Entity = entity;
            this.Delta = delta;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the delta.
        /// </summary>
        public Vector2 Delta { get; private set; }
        #endregion
    }
}
