using System.IO;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Fonts.Utility;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Content.Serialization
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
                int kerning = reader.ReadInt32();
                int spacing = reader.ReadInt32();

                InternalFontCache fontCache = new InternalFontCache();
                fontCache.Deserialize(reader.BaseStream);

                SpriteFont spriteFont = SpriteFontGenerator.Create(factory, fontCache.Data);
                spriteFont.Kerning = kerning;
                spriteFont.Spacing = spacing;
                spriteFont.FontCache = fontCache;

                return spriteFont;
            }

            return null;
        }
        #endregion
    }
}
