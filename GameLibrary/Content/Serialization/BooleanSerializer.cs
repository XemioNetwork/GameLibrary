using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class BooleanSerializer : Serializer<bool>
    {
        #region Overrides of SerializationManager<bool>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override bool Read(IFormatReader reader)
        {
            return reader.ReadBoolean("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, bool value)
        {
            writer.WriteBoolean("Value", value);
        }
        #endregion
    }
}
