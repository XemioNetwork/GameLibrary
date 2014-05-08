using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public class ScaleEffect : IEffect
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleEffect"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public ScaleEffect(float x, float y) : this(new Vector2(x, y))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleEffect"/> class.
        /// </summary>
        /// <param name="scale">The scale.</param>
        public ScaleEffect(Vector2 scale)
        {
            this.Scale = scale;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the scale.
        /// </summary>
        public Vector2 Scale { get; private set; }
        #endregion
    }
}
