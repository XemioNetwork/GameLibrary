using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.GdiPlus.Geometry
{
    public class GdiBrush : IBrush
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiBrush"/> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        public GdiBrush(Brush brush)
        {
            this.Brush = brush;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the brush.
        /// </summary>
        public Brush Brush { get; private set; }
        #endregion
        
        #region IBrush Member
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
