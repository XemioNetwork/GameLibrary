﻿using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class ShortSerializer : Serializer<short>
    {
        #region Overrides of SerializationManager<short>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override short Read(IFormatReader reader)
        {
            return reader.ReadShort("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, short value)
        {
            writer.WriteShort("Value", value);
        }
        #endregion
    }
}
