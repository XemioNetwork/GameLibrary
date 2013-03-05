using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Math
{
    public class BoundingBox : IIntersectable<BoundingBox>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> class.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        public BoundingBox(Vector2 min, Vector2 max)
        {
            this.Min = min;
            this.Max = max;
        }
        #endregion

        #region Fields
        private Vector2 _min;
        private Vector2 _max;
        #endregion

        #region Constants
        /// <summary>
        /// Gets the radiant value for the right top angle.
        /// </summary>
        private const float rightTopAngle = 5.49778714f;
        /// <summary>
        /// Gets the radiant value for the right bottom angle.
        /// </summary>
        private const float rightBottomAngle = 0.785398163f;
        /// <summary>
        /// Gets the radiant value for the left bottom angle.
        /// </summary>
        private const float leftBottomAngle = 2.35619449f;
        /// <summary>
        /// Gets the radiant value for the left top angle.
        /// </summary>
        private const float leftTopAngle = 3.92699082f;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the upper left vector.
        /// </summary>
        public Vector2 Min
        {
            get { return this._min; }
            set
            {
                this._min = value;
                this.CalculateBounds();
            }
        }
        /// <summary>
        /// Gets or sets the bottom right vector.
        /// </summary>
        public Vector2 Max 
        {
            get { return this._max; }
            set
            {
                this._max = value;
                this.CalculateBounds();
            }
        }
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the bounds.
        /// </summary>
        private void CalculateBounds()
        {
            this.Bounds = new Rectangle(this.Min, this.Max);
        }
        #endregion

        #region IIntersectable<BoundingBox> Member
        /// <summary>
        /// Determines wether the specified bounding boxes intersect.
        /// </summary>
        /// <param name="boundingBox">The bounding box.</param>
        public bool Intersects(BoundingBox boundingBox)
        {
            return boundingBox.Bounds.Intersects(this.Bounds);
        }
        /// <summary>
        /// Intersects the specified bounding box.
        /// </summary>
        /// <param name="boundingBox">The bounding box.</param>
        public Vector2 Intersect(BoundingBox boundingBox)
        {
            Vector2 minimumTranslation = Vector2.Zero;

            if (this.Intersects(boundingBox))
            {
                Rectangle overlap = this.Bounds.Intersect(boundingBox.Bounds);

                Vector2 a = this.Bounds.Center;
                Vector2 b = boundingBox.Bounds.Center;

                Vector2 direction = Vector2.Normalize(a - b);
                float angle = MathHelper.ToAngle(direction);

                if (angle >= rightTopAngle || angle < rightBottomAngle)
                {
                    minimumTranslation = new Vector2(-overlap.Width, 0);
                }
                else if (angle >= rightBottomAngle && angle < leftBottomAngle)
                {
                    minimumTranslation = new Vector2(0, overlap.Height);
                }
                else if (angle >= leftBottomAngle && angle < leftTopAngle)
                {
                    minimumTranslation = new Vector2(overlap.Width, 0);
                }
                else if (angle >= leftTopAngle && angle < rightTopAngle)
                {
                    minimumTranslation = new Vector2(0, -overlap.Height);
                }
            }

            return minimumTranslation;
        }
        #endregion
    }
}
