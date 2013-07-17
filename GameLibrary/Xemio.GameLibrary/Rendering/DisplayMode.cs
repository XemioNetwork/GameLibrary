using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering
{
    public class DisplayMode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayMode"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public DisplayMode(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DisplayMode"/> is scaling.
        /// </summary>
        public bool Scaling { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Gets the bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, this.Width, this.Height); }
        }
        /// <summary>
        /// Gets the center of the screen.
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(this.Width * .5f, this.Height * .5f); }
        }
        #endregion
    }
}
