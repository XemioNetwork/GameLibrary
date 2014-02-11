using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.References
{
    internal class DerivableReferenceElement : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceElement"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public DerivableReferenceElement(string tag, PropertyInfo property) : base(tag, property.GetValue, property.SetValue)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public DerivableReferenceElement(string tag, Func<object, object> getAction, Action<object, object> setAction) : base(tag, getAction, setAction)
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
                object propertyValue = this.GetAction(container);

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

                this.SetAction(container, propertyValue);
            }
        }
        #endregion
    }
}
