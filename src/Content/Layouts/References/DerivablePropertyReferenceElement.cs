using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.References
{
    internal class DerivablePropertyReferenceElement : PropertyElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyReferenceElement"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public DerivablePropertyReferenceElement(string tag, PropertyInfo property) : base(tag, property)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the serializer.
        /// </summary>
        protected SerializationManager Serializer
        {
            get { return XGL.Components.Require<SerializationManager>(); }
        }
        #endregion

        #region Overrides of PropertyElement
        /// <summary>
        /// Writes property for the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public override void Write(IFormatWriter writer, object container)
        {
            using (writer.Section(this.Tag))
            {
                object propertyValue = this.Property.GetValue(container);

                writer.WriteString("Type", propertyValue.GetType().AssemblyQualifiedName);
                this.Serializer.Save(propertyValue, writer);
            }
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            using (reader.Section(this.Tag))
            {
                Type type = Type.GetType(reader.ReadString("Type"));
                object propertyValue = this.Serializer.Load(type, reader);

                this.Property.SetValue(container, propertyValue);
            }
        }
        #endregion
    }
}
