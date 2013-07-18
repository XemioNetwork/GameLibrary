using System.IO;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class StringSerializer : ContentSerializer<string>
    {
        #region Overrides of ContentSerializer<string>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override string Read(BinaryReader reader)
        {
            return reader.ReadString();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, string value)
        {
            writer.Write(value);
        }
        #endregion
    }
}
