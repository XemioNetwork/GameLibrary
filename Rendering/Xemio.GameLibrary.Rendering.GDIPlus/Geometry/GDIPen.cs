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
    using Drawing = System.Drawing;
    using Color = Xemio.GameLibrary.Rendering.Color;

    public class GDIPen : IPen
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIPen"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public GDIPen(Color color, float thickness)
        {
            this.Color = color;
            this.Thickness = thickness;

            this._nativePen = new Pen(
                new SolidBrush(GDIHelper.Convert(color)), thickness);
        }
        #endregion

        #region Fields
        private Pen _nativePen;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the native pen.
        /// </summary>
        public Pen GetNativePen()
        {
            return this._nativePen;
        }
        #endregion

        #region IPen Member
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; private set; }
        /// <summary>
        /// Gets the thickness.
        /// </summary>
        public float Thickness { get; private set; }
        #endregion
    }
}
