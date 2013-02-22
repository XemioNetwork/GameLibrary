using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.GDIPlus
{
    public class GDITexture : ITexture
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDITexture"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="pixels">The pixels.</param>
        public GDITexture(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the bitmap.
        /// </summary>
        public Bitmap Bitmap { get; private set; }
        #endregion

        #region ITexture Member
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return this.Bitmap.Width; }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return this.Bitmap.Height; }
        }
        #endregion
    }
}
