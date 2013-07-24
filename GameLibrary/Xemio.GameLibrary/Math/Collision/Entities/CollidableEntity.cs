using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math.Collision.Sources;

namespace Xemio.GameLibrary.Math.Collision.Entities
{
    public class CollidableEntity : Entity, ICollisionSource
    {
        #region Methods
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public override void Initialize(EntityEnvironment environment)
        {
            var collisionEnvironment = environment as CollisionEnvironment;
            var collisionComponent = this.GetComponent<CollisionComponent>();

            if (collisionEnvironment != null &&
                collisionComponent != null)
            {
                collisionEnvironment.Grid.Update(this, collisionComponent.CollisionMap);
            }
        }
        #endregion
    }
}
