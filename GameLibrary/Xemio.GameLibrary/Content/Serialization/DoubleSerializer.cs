﻿using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class DoubleSerializer : ContentSerializer<double>
    {
        #region Overrides of ContentSerializer<double>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override double Read(IFormatReader reader)
        {
            return reader.ReadDouble();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, double value)
        {
            writer.WriteDouble("Double", value);
        }
        #endregion
    }
}
