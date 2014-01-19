using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class GuidElement : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public GuidElement(string tag, string guidFormat, PropertyInfo property) : this(tag, guidFormat, property.GetValue, property.SetValue)
        {
            this.GuidFormat = guidFormat;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public GuidElement(string tag, string guidFormat, Func<object, object> getAction, Action<object, object> setAction) : base(tag, getAction, setAction)
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
            var guid = (Guid)this.GetAction(container);
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
            this.SetAction(container, Guid.Parse(reader.ReadString(this.Tag)));
        }
        #endregion
    }
}
