using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Content.IO
{
    public class FontReader : ContentReader<SpriteFont>
    {
        #region ContentReader<SpriteFont> Member
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override SpriteFont Read(BinaryReader reader)
        {
            ITextureFactory factory = XGL.GetComponent<ITextureFactory>();
            if (factory != null)
            {
                return SpriteFont.Load(factory, reader.BaseStream);
            }

            return null;
        }
        #endregion
    }
}
