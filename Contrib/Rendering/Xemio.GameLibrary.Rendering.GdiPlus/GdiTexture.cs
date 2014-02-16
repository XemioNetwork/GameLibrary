using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    public class GdiTexture : ITexture
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiTexture" /> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public GdiTexture(Bitmap bitmap)
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
        /// <summary>
        /// Gets the texture data.
        /// </summary>
        public byte[] GetData()
        {
            var stream = new MemoryStream(this.Bitmap.Width * this.Bitmap.Height * 4);
            this.Bitmap.Save(stream, ImageFormat.Png);

            stream.Position = 0;

            return stream.ToArray();
        }
        /// <summary>
        /// Sets the texture data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetData(byte[] data)
        {
            this.Bitmap = new Bitmap(new MemoryStream(data));
        }
        #endregion
    }
}
