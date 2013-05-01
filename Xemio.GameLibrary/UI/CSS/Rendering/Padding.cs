using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.CSS.Rendering
{
    public class Padding
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Padding"/> class.
        /// </summary>
        public Padding()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Padding"/> class.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="left">The left.</param>
        public Padding(int top, int right, int bottom, int left)
        {
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.Left = left;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public int Right { get; set; }
        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        public int Bottom { get; set; }
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public int Left { get; set; }
        #endregion
    }
}
