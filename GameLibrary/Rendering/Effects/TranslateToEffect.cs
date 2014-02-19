using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public class TranslateToEffect : IEffect
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateToEffect"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public TranslateToEffect(Vector2 position)
        {
            this.Position = position;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        #endregion
    }
}
