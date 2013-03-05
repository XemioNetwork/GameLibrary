using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Fonts.Utility
{
    public static class SpriteFontSerializer
    {
        #region Methods
        /// <summary>
        /// Saves the specified font.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void Save(SpriteFont font, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                SpriteFontSerializer.Save(font, stream);
            }
        }
        /// <summary>
        /// Saves the specified font.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="stream">The stream.</param>
        public static void Save(SpriteFont font, Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(font.Kerning);
                writer.Write(font.Spacing);

                font.FontCache.Serialize(stream);
            }
        }
        #endregion
    }
}
