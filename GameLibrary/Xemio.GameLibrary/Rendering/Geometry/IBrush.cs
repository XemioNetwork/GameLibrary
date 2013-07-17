using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Geometry
{
    public interface IBrush
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        int Height { get; }
    }
}
