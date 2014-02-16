using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Serialization;

namespace Xemio.GameLibrary.Rendering.HTML5.Serialization
{
    public class HTMLTextureReader : TextureReader
    {
        #region Overrides of TextureReader
        /// <summary>
        /// Reads a texture.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override ITexture Read(IFormatReader reader)
        {
            return new HTMLTexture();
        }
        #endregion
    }
}
