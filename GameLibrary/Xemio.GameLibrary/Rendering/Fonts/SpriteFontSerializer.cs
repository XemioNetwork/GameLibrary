using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Rendering.Fonts
{
    public class SpriteFontSerializer : ContentSerializer<SpriteFont>
    {
        #region Overrides of ContentSerializer<SpriteFont>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override SpriteFont Read(BinaryReader reader)
        {
            SpriteFont font = new SpriteFont();

            font.Kerning = reader.ReadInt32();
            font.Spacing = reader.ReadInt32();

            font.Bitmaps = new Bitmap[reader.ReadInt32()];

            for (int i = 0; i < font.Bitmaps.Length; i++)
            {
                if (!reader.ReadBoolean())
                {
                    int length = (int)reader.ReadInt64();
                    byte[] binaryData = reader.ReadBytes(length);

                    using (MemoryStream memory = new MemoryStream())
                    {
                        memory.Write(binaryData, 0, binaryData.Length);
                        memory.Seek(0, SeekOrigin.Begin);

                        font.Bitmaps[i] = Image.FromStream(memory) as Bitmap;
                    }
                }
            }

            return font;
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, SpriteFont value)
        {
            writer.Write(value.Kerning);
            writer.Write(value.Spacing);

            writer.Write(value.Bitmaps.Length);

            foreach (Bitmap bitmap in value.Bitmaps)
            {
                writer.Write(bitmap == null);
                if (bitmap != null)
                {
                    using (MemoryStream memory = new MemoryStream())
                    {
                        bitmap.Save(memory, ImageFormat.Png);

                        writer.Write(memory.Length);
                        writer.Write(memory.ToArray());
                    }
                }
            }
        }
        #endregion
    }
}
