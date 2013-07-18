using System.IO;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class ByteSerializer : ContentSerializer<byte>
    {
        #region Overrides of ContentSerializer<byte>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override byte Read(BinaryReader reader)
        {
            return reader.ReadByte();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }
        #endregion
    }
}
