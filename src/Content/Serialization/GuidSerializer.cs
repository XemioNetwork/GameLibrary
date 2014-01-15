using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class GuidSerializer : Serializer<Guid>
    {
        #region Overrides of Serializer<Guid>
        /// <summary>
        /// Reads the specified guid.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Guid Read(IFormatReader reader)
        {
            return Guid.Parse(reader.ReadString("Value"));
        }
        /// <summary>
        /// Writes the specified guid.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, Guid value)
        {
            writer.WriteString("Value", value.ToString());
        }
        #endregion
    }
}
