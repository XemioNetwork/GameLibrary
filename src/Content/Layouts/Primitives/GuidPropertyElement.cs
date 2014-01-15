using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class GuidPropertyElement : PropertyElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidPropertyElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public GuidPropertyElement(string tag, string guidFormat, PropertyInfo property) : base(tag, property)
        {
            this.GuidFormat = guidFormat;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the format.
        /// </summary>
        public string GuidFormat { get; private set; }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes property for the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public override void Write(IFormatWriter writer, object container)
        {
            var guid = (Guid)this.Property.GetValue(container);
            string content = guid.ToString(this.GuidFormat);

            writer.WriteString(this.Tag, content);
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            this.Property.SetValue(container, Guid.Parse(reader.ReadString(this.Tag)));
        }
        #endregion
    }
}
