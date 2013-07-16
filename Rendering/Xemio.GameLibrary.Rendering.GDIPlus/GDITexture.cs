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

namespace Xemio.GameLibrary.Rendering.GDIPlus
{
    public class GDITexture : ITexture
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDITexture" /> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
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
        /// <summary>
        /// Gets the texture data.
        /// </summary>
        public byte[] GetData()
        {
            BitmapData data = this.Bitmap.LockBits(
                new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height),
                ImageLockMode.ReadOnly,
                this.Bitmap.PixelFormat);

            byte[] result = new byte[data.Width * data.Height * 4];
            Marshal.Copy(data.Scan0, result, 0, result.Length);

            this.Bitmap.UnlockBits(data);

            return result;
        }
        /// <summary>
        /// Sets the texture data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetData(byte[] data)
        {
            BitmapData bm = this.Bitmap.LockBits(
                new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height),
                ImageLockMode.WriteOnly,
                this.Bitmap.PixelFormat);

            Marshal.Copy(data, 0, bm.Scan0, data.Length);
            this.Bitmap.UnlockBits(bm);
        }
        #endregion
    }
}
