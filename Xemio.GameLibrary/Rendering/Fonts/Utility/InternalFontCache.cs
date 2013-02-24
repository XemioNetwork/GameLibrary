using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Xemio.GameLibrary.Rendering.Fonts.Utility
{
    public class InternalFontCache
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalFontCache"/> class.
        /// </summary>
        public InternalFontCache()
        {
            this.Data = new Bitmap[byte.MaxValue];
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the bitmap data.
        /// </summary>
        public Bitmap[] Data { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Serializes the specified font unit.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Serialize(Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(this.Data.Length);

                for (int i = 0; i < this.Data.Length; i++)
                {
                    Bitmap bitmap = this.Data[i];
                    
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
        }
        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Deserialize(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                this.Data = new Bitmap[reader.ReadInt32()];

                for (int i = 0; i < this.Data.Length; i++)
                {
                    if (!reader.ReadBoolean())
                    {
                        int length = (int)reader.ReadInt64();
                        byte[] binaryData = reader.ReadBytes(length);

                        using (MemoryStream memory = new MemoryStream())
                        {
                            memory.Write(binaryData, 0, binaryData.Length);
                            memory.Seek(0, SeekOrigin.Begin);

                            this.Data[i] = Bitmap.FromStream(memory) as Bitmap;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
