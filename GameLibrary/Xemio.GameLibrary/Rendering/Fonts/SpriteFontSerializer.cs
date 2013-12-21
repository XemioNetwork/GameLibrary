using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Rendering.Fonts
{
    public class SpriteFontSerializer : Serializer<SpriteFont>
    {
        #region Overrides of SerializationManager<SpriteFont>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override SpriteFont Read(IFormatReader reader)
        {
            SpriteFont font = new SpriteFont
                                  {
                                      Kerning = reader.ReadInteger(),
                                      Spacing = reader.ReadInteger(),
                                      Bitmaps = new Bitmap[reader.ReadInteger()]
                                  };

            for (int i = 0; i < font.Bitmaps.Length; i++)
            {
                if (!reader.ReadBoolean())
                {
                    int length = (int)reader.ReadLong();
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
        public override void Write(IFormatWriter writer, SpriteFont value)
        {
            writer.WriteInteger("Kerning", value.Kerning);
            writer.WriteInteger("Spacing", value.Spacing);

            writer.WriteLong("BitmapCount", value.Bitmaps.Length);

            foreach (Bitmap bitmap in value.Bitmaps)
            {
                writer.WriteBoolean("IsBitmapNull", bitmap == null);
                if (bitmap != null)
                {
                    using (MemoryStream memory = new MemoryStream())
                    {
                        bitmap.Save(memory, ImageFormat.Png);

                        writer.WriteLong("Length", memory.Length);
                        writer.WriteBytes("Data", memory.ToArray());
                    }
                }
            }
        }
        #endregion
    }
}
