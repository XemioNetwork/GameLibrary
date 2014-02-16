using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Serialization;

namespace Xemio.GameLibrary.Rendering.GdiPlus.Serialization
{
    public class GdiTextureReader : TextureReader
    {
        #region Overrides of TextureReader
        /// <summary>
        /// Reads a gdi texture.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override ITexture Read(IFormatReader reader)
        {
            var bitmap = (Bitmap)Image.FromStream(reader.Stream);
            var clone = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);

            using (Graphics g = Graphics.FromImage(clone))
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            }

            return new GdiTexture(clone);
        }
        #endregion
    }
}
