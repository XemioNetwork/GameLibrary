using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Surfaces
{
    internal class NullSurface : ISurface
    {
        #region Implementation of ISurface
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        #endregion
    }
}
