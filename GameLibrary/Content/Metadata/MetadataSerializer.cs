using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Layouts;

namespace Xemio.GameLibrary.Content.Metadata
{
    public class MetadataSerializer : Serializer<IMetadata>
    {
        #region Overrides of Serializer<IMetadata>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override IMetadata Read(IFormatReader reader)
        {
            string typeName = reader.ReadString("Metadata");
            Type type = Type.GetType(typeName);

            using (reader.Section("Data"))
            {
                return (IMetadata)new AutomaticLayoutSerializer(type).Read(reader);
            }
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, IMetadata value)
        {
            writer.WriteString("Metadata", value.GetType().AssemblyQualifiedName);
            using (writer.Section("Data"))
            {
                new AutomaticLayoutSerializer(value.GetType()).Write(writer, value);
            }
        }
        #endregion
    }
}
