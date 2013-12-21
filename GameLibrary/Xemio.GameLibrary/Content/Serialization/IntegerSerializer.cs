using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class IntegerSerializer : Serializer<int>
    {
        #region Overrides of ContentSerializer<int>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override int Read(IFormatReader reader)
        {
            return reader.ReadInteger();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, int value)
        {
            writer.WriteInteger("Integer", value);
        }
        #endregion
    }
}
