using System.IO;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class ShortSerializer : ContentSerializer<short>
    {
        #region Overrides of ContentSerializer<short>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override short Read(BinaryReader reader)
        {
            return reader.ReadInt16();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, short value)
        {
            writer.Write(value);
        }
        #endregion
    }
}
