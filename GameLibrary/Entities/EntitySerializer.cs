using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Layouts;
using Xemio.GameLibrary.Content.Layouts.Collections;
using Xemio.GameLibrary.Entities.Components;

namespace Xemio.GameLibrary.Entities
{
    public class EntitySerializer : LayoutSerializer<Entity>
    {
        #region Methods
        /// <summary>
        /// Writes the components.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="components">The components.</param>
        private void WriteComponents(Entity entity, ICollection<EntityComponent> components)
        {
            entity.Components.Clear();
            foreach (EntityComponent component in components)
            {
                entity.AddComponent(component);
            }
        } 
        #endregion

        #region Overrides of Serializer<Entity>
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, Entity value)
        {
            writer.WriteString("Type", value.GetType().AssemblyQualifiedName);
            base.Write(writer, value);
        }
        /// <summary>
        /// Creates a new instance inside the reading process.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override Entity CreateInstance(IFormatReader reader)
        {
            Type type = Type.GetType(reader.ReadString("Type"));
            var entity = (Entity)Activator.CreateInstance(type, true);

            return entity;
        }
        /// <summary>
        /// Creates the layout.
        /// </summary>
        public override PersistenceLayout<Entity> CreateLayout()
        {
            return new PersistenceLayout<Entity>()
                .Element(e => e.Guid)
                .Element(e => e.IsDestroyed)
                .Element(e => e.IsVisible)
                .Collection("Component", "Components", e => e.Components, this.WriteComponents, DerivableScope.Element);
        }
        #endregion
    }
}
