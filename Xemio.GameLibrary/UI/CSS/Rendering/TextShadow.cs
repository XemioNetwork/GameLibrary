using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.UI.CSS.Rendering
{
    public class TextShadow
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TextShadow"/> class.
        /// </summary>
        public TextShadow()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextShadow"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="color">The color.</param>
        public TextShadow(Vector2 offset, Color color)
        {
            this.Offset = offset;
            this.Color = color;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets the text shadow declaration for no shadow.
        /// </summary>
        public static TextShadow None
        {
            get { return Singleton<TextShadow>.Value; }
        }
        #endregion
    }
}
