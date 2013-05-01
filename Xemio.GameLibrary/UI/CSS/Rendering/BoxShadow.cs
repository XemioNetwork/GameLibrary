using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.UI.CSS.Rendering
{
    public class BoxShadow
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BoxShadow"/> class.
        /// </summary>
        public BoxShadow()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BoxShadow"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="bluriness">The bluriness.</param>
        /// <param name="color">The color.</param>
        public BoxShadow(Vector2 offset, int bluriness, Color color)
        {
            this.Offset = offset;
            this.Bluriness = bluriness;
            this.Color = color;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        /// <summary>
        /// Gets or sets the bluriness.
        /// </summary>
        public int Bluriness { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets the box shadow declaration for no box shadow.
        /// </summary>
        public static BoxShadow None
        {
            get { return Singleton<BoxShadow>.Value; }
        }
        #endregion
    }
}
