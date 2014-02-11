using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public class TintEffect : IEffect
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TintEffect"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public TintEffect(Color color)
        {
            this.Color = color;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; private set; }
        #endregion
    }
}
