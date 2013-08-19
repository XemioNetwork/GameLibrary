using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class StringSerializer : ContentSerializer<string>
    {
        #region Overrides of ContentSerializer<string>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override string Read(IFormatReader reader)
        {
            return reader.ReadString();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, string value)
        {
            writer.WriteString("String", value);
        }
        #endregion
    }
}
