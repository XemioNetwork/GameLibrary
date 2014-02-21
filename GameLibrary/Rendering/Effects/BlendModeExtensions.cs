using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public static class BlendModeExtensions
    {
        #region Methods
        /// <summary>
        /// Combines the specified colors.
        /// </summary>
        /// <param name="blendMode">The blend mode.</param>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        public static Color Combine(this BlendMode blendMode, Color a, Color b)
        {
            switch (blendMode)
            {
                case BlendMode.Override:
                    return b;
                case BlendMode.Add:
                    return Color.Add(a, b);
                case BlendMode.Multiply:
                    return Color.Multiply(a, b);
                case BlendMode.Divide:
                    return Color.Divide(a, b);
                case BlendMode.Subtract:
                    return Color.Subtract(a, b);
                case BlendMode.LightenOnly:
                    return Color.Lighten(a, b);
                case BlendMode.DarkenOnly:
                    return Color.Darken(a, b);
                default:
                    throw new ArgumentOutOfRangeException("blendMode");
            }
        }
        #endregion
    }
}
