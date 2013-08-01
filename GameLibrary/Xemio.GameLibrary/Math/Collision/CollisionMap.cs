using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Math.Collision
{
    public class CollisionMap
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public CollisionMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.Cells = new bool[width, height];
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Gets or sets the cells.
        /// </summary>
        public bool[,] Cells { get; private set; }
        #endregion
    }
}
