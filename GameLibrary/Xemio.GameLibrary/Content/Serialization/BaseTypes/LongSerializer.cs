using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.Serialization.BaseTypes
{
    public class LongSerializer : ContentSerializer<long>
    {
        #region Overrides of ContentSerializer<long>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override long Read(BinaryReader reader)
        {
            return reader.ReadInt64();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, long value)
        {
            writer.Write(value);
        }
        #endregion
    }
}
