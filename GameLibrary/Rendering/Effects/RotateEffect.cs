using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public class RotateEffect : IEffect
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RotateEffect"/> class.
        /// </summary>
        /// <param name="angle">The angle.</param>
        public RotateEffect(float angle)
        {
            this.Angle = angle;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the angle measured in radians.
        /// </summary>
        public float Angle { get; private set; }
        #endregion
    }
}
