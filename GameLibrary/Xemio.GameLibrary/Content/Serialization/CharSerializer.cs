using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class CharSerializer : Serializer<char>
    {
        #region Overrides of ContentSerializer<char>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override char Read(IFormatReader reader)
        {
            return reader.ReadCharacter();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, char value)
        {
            writer.WriteCharacter("Character", value);
        }
        #endregion
    }
}
