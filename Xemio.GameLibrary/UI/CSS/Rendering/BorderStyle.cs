using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.UI.CSS.Rendering
{
    public class BorderStyle
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BorderStyle"/> class.
        /// </summary>
        public BorderStyle() : this(1, Color.Transparent)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BorderStyle"/> class.
        /// </summary>
        /// <param name="thickness">The thickness.</param>
        /// <param name="color">The color.</param>
        public BorderStyle(float thickness, Color color)
        {
            this.Thickness = thickness;
            this.Color = color;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        public float Thickness { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets the border style for no border.
        /// </summary>
        public static BorderStyle None
        {
            get { return Singleton<BorderStyle>.Value; }
        }
        #endregion
    }
}
