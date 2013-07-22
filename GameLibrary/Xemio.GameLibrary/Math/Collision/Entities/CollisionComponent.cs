using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.GameLibrary.Math.Collision.Entities
{
    public class CollisionComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="collisionMap">The collision map.</param>
        public CollisionComponent(Entity entity, CollisionMap collisionMap) : base(entity)
        {
            this.CollisionMap = collisionMap;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collision map.
        /// </summary>
        public CollisionMap CollisionMap { get; private set; }
        #endregion
    }
}
