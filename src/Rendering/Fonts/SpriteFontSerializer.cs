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
            var font = new SpriteFont
                            {
                                Kerning = reader.ReadInteger("Kerning"),
                                Spacing = reader.ReadInteger("Spacing"),
                                Bitmaps = new Bitmap[reader.ReadInteger("BitmapCount")]
                            };

            using (reader.Section("Bitmaps"))
            {
                for (int i = 0; i < font.Bitmaps.Length; i++)
                {
                    using (reader.Section("Bitmap"))
                    {
                        if (!reader.ReadBoolean("IsBitmapNull"))
                        {
                            using (var memory = new MemoryStream(reader.ReadBytes("Data")))
                            {
                                font.Bitmaps[i] = (Bitmap)Image.FromStream(memory);
                            }
                        }
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

            using (writer.Section("Bitmaps"))
            {
                foreach (Bitmap bitmap in value.Bitmaps)
                {
                    using (writer.Section("Bitmap"))
                    {
                        writer.WriteBoolean("IsBitmapNull", bitmap == null);
                        if (bitmap != null)
                        {
                            using (var memory = new MemoryStream())
                            {
                                bitmap.Save(memory, ImageFormat.Png);
                                writer.WriteBytes("Data", memory.ToArray());
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
