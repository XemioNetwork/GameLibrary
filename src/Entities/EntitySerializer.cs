using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Entities.Components;

namespace Xemio.GameLibrary.Entities
{
    public class EntitySerializer : Serializer<Entity>
    {
        #region Overrides of Serializer<Entity>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Entity Read(IFormatReader reader)
        {
            var serializer = XGL.Components.Require<SerializationManager>();

            Type type = Type.GetType(reader.ReadString("Type"));
            var entity = (Entity)Activator.CreateInstance(type);

            using (reader.Section("Guid"))
            {
                entity.Guid = serializer.Load<Guid>(reader);
            }

            if (reader.ReadBoolean("IsNamed"))
            {
                entity.Name = reader.ReadString("Name");
            }

            entity.IsDestroyed = reader.ReadBoolean("IsDestroyed");
            entity.IsVisible = reader.ReadBoolean("IsVisible");

            using (reader.Section("Components"))
            {
                entity.Components.Clear();

                var components = serializer.Load<List<EntityComponent>>(reader);
                foreach (EntityComponent component in components)
                {
                    entity.Add(component);
                }
            }

            return entity;
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="entity">The entity.</param>
        public override void Write(IFormatWriter writer, Entity entity)
        {
            var serializer = XGL.Components.Require<SerializationManager>();
            
            writer.WriteString("Type", entity.GetType().AssemblyQualifiedName);

            using (writer.Section("Guid"))
            {
                serializer.Save(entity.Guid, writer);
            }

            writer.WriteBoolean("IsNamed", entity.IsNamed);
            if (entity.IsNamed)
            {
                writer.WriteString("Name", entity.Name);
            }

            writer.WriteBoolean("IsDestroyed", entity.IsDestroyed);
            writer.WriteBoolean("IsVisible", entity.IsVisible);

            using (writer.Section("Components"))
            {
                serializer.Save(entity.Components, writer);
            }
        }
        #endregion
    }
}
