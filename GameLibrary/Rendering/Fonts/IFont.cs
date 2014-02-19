using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Fonts
{
    public interface IFont
    {
        /// <summary>
        /// Gets the font family.
        /// </summary>
        string FontFamily { get; }
        /// <summary>
        /// Gets the size.
        /// </summary>
        float Size { get; }
        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="value">The value.</param>
        Vector2 MeasureString(string value);
    }
}
