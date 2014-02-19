using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Serialization;

namespace Xemio.GameLibrary.Rendering.GdiPlus.Serialization
{
    public class GdiTextureWriter : TextureWriter
    {
        #region Overrides of TextureWriter
        /// <summary>
        /// Writes the specified texture.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, ITexture value)
        {
            var texture = (GdiTexture)value;
            texture.Bitmap.Save(writer.Stream, ImageFormat.Png);
        }
        #endregion
    }
}
