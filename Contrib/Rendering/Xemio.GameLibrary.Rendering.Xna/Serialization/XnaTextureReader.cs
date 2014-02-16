using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Serialization;

namespace Xemio.GameLibrary.Rendering.Xna.Serialization
{
    public class XnaTextureReader : TextureReader
    {
        #region Overrides of TextureReader
        /// <summary>
        /// Reads a texture out of the specified stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override ITexture Read(IFormatReader reader)
        {
            return new XnaTexture(Texture2D.FromStream(XnaHelper.GraphicsDevice, reader.Stream));
        }
        #endregion
    }
}
