using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Math
{
    public class BoundingSphere : IIntersectable<BoundingSphere>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingSphere"/> class.
        /// </summary>
        public BoundingSphere()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingSphere"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public BoundingSphere(Vector2 position, float radius)
        {
            this.Position = position;
            this.Radius = radius;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        public float Radius { get; set; }
        #endregion

        #region IIntersectable<BoundingSphere> Member
        /// <summary>
        /// Determines wether the two spheres are intersecting.
        /// </summary>
        /// <param name="sphere">The sphere.</param>
        public bool Intersects(BoundingSphere sphere)
        {
            float totalRadius = this.Radius + sphere.Radius;
            float radiusSquared = totalRadius * totalRadius;
            
            return (this.Position - sphere.Position).LengthSquared < radiusSquared;
        }
        /// <summary>
        /// Returns the minimum translation vector for the specified sphere-sphere intsection.
        /// </summary>
        /// <param name="sphere">The sphere.</param>
        public Vector2 Intersect(BoundingSphere sphere)
        {
            if (!this.Intersects(sphere))
            {
                return Vector2.Zero;
            }

            Vector2 distance = this.Position - sphere.Position;
            float totalRadius = this.Radius + sphere.Radius;

            return Vector2.Normalize(distance) * (totalRadius - distance.Length);
        }
        #endregion
    }
}
