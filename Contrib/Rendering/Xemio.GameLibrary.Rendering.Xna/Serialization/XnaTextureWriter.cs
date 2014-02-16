using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Serialization;

namespace Xemio.GameLibrary.Rendering.Xna.Serialization
{
    public class XnaTextureWriter : TextureWriter
    {
        #region Overrides of TextureWriter
        /// <summary>
        /// Writes the specified texture.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, ITexture value)
        {
            XnaTexture texture = (XnaTexture)value;
            texture.Texture.SaveAsPng(writer.Stream, texture.Width, texture.Height);
        }
        #endregion
    }
}
