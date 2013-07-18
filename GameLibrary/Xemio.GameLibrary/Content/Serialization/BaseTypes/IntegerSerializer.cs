using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.Serialization.BaseTypes
{
    public class IntegerSerializer : ContentSerializer<int>
    {
        #region Overrides of ContentSerializer<int>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override int Read(BinaryReader reader)
        {
            return reader.ReadInt32();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, int value)
        {
            writer.Write(value);
        }
        #endregion
    }
}
