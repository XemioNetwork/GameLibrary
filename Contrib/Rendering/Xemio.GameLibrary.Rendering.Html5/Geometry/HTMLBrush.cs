using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.HTML5.Geometry
{
    public class HTMLBrush : IBrush
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HTMLBrush"/> class.
        /// </summary>
        public HTMLBrush()
        {
        }
        #endregion
        
        #region Implementation of IBrush
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
