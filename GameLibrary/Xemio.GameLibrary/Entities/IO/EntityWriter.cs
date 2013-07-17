using System;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Common.Extensions;

namespace Xemio.GameLibrary.Entities.IO
{
    public class EntityWriter : ContentWriter<Entity>
    {
        #region Overrides of ContentWriter<Entity>
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, Entity value)
        {
            Type type = value.GetType();
            string typeName = type.AssemblyQualifiedName;

            writer.Write(typeName);

            writer.Write(value.Position);
            writer.Write(value.Containers.Count);

            foreach (EntityDataContainer container in value.Containers)
            {
                Type containerType = container.GetType();
                string containerTypeName = containerType.AssemblyQualifiedName;

                writer.Write(containerTypeName);
                writer.WriteInstance(container);
            }
        }
        #endregion
    }
}
