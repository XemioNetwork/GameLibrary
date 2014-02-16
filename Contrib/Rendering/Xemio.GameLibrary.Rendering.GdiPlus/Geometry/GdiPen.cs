using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.GdiPlus.Geometry
{
    using Drawing = System.Drawing;
    using Color = Xemio.GameLibrary.Rendering.Color;

    public class GdiPen : IPen
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiPen"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public GdiPen(Color color, float thickness)
        {
            this.Color = color;
            this.Thickness = thickness;

            this.Pen = new Pen(new SolidBrush(GdiHelper.Convert(color)), thickness);
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the pen.
        /// </summary>
        public Pen Pen { get; private set; }
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
