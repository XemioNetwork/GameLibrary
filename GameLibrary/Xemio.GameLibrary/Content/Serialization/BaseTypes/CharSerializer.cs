using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.Serialization.BaseTypes
{
    public class CharSerializer : ContentSerializer<char>
    {
        #region Overrides of ContentSerializer<char>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override char Read(BinaryReader reader)
        {
            return reader.ReadChar();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, char value)
        {
            writer.Write(value);
        }
        #endregion
    }
}
