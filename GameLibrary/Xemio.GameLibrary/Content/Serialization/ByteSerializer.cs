using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class ByteSerializer : Serializer<byte>
    {
        #region Overrides of ContentSerializer<byte>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override byte Read(IFormatReader reader)
        {
            return reader.ReadByte();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, byte value)
        {
            writer.WriteByte("Byte", value);
        }
        #endregion
    }
}
