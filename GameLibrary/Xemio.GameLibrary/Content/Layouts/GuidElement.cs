using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class GuidElement : PropertyElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public GuidElement(string tag, string guidFormat, PropertyInfo property) : base(tag, property)
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
        /// Writes property for the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, object value)
        {
            var guid = (Guid)this.Property.GetValue(value);
            string content = guid.ToString(this.GuidFormat);

            writer.WriteString(this.Tag, content);
        }
        /// <summary>
        /// Reads the property for the specified value.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        public override void Read(IFormatReader reader, object value)
        {
            this.Property.SetValue(value, Guid.Parse(reader.ReadString(this.Tag)));
        }
        #endregion
    }
}
