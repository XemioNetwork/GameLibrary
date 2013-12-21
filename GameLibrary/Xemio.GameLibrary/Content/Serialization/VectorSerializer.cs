using System.IO;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class VectorSerializer : Serializer<Vector2>
    {
        #region Overrides of SerializationManager<Vector2>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Vector2 Read(IFormatReader reader)
        {
            return reader.ReadVector2();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, Vector2 value)
        {
            writer.WriteVector2("Vector", value);
        }
        #endregion
    }
}
