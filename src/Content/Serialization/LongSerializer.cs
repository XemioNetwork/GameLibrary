using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class LongSerializer : Serializer<long>
    {
        #region Overrides of SerializationManager<long>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override long Read(IFormatReader reader)
        {
            return reader.ReadLong("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, long value)
        {
            writer.WriteLong("Value", value);
        }
        #endregion
    }
}
