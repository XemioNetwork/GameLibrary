using System.IO;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class DoubleSerializer : ContentSerializer<double>
    {
        #region Overrides of ContentSerializer<double>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override double Read(BinaryReader reader)
        {
            return reader.ReadDouble();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, double value)
        {
            writer.Write(value);
        }
        #endregion
    }
}
