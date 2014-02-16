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
        public TintEffect(Color color) : this(color, BlendMode.Add)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TintEffect" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="blendMode">The blend mode.</param>
        public TintEffect(Color color, BlendMode blendMode)
        {
            this.Color = color;
            this.BlendMode = blendMode;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; private set; }
        /// <summary>
        /// Gets the blend mode.
        /// </summary>
        public BlendMode BlendMode { get; private set; }
        #endregion
    }
}
