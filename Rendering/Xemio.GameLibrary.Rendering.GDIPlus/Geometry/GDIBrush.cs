using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.GDIPlus.Geometry
{
    public class GDIBrush : IBrush
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIBrush"/> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        public GDIBrush(Brush brush)
        {
            this._nativeBrush = brush;
        }
        #endregion

        #region Fields
        private Brush _nativeBrush;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the native brush.
        /// </summary>
        /// <returns></returns>
        public Brush GetNativeBrush()
        {
            return this._nativeBrush;
        }
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
