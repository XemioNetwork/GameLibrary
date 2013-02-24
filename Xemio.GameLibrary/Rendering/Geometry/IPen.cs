using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Geometry
{
    public interface IPen
    {
        /// <summary>
        /// Gets the color.
        /// </summary>
        Color Color { get; }
        /// <summary>
        /// Gets the thickness.
        /// </summary>
        float Thickness { get; }
    }
}
