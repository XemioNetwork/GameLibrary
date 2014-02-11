using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public class TranslateEffect : IEffect
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateEffect"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public TranslateEffect(Vector2 offset)
        {
            this.Offset = offset;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public Vector2 Offset { get; private set; }
        #endregion
    }
}
