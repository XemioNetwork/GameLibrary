using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class SectionElement<T> : ILayoutElement
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionElement{T}"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="layout">The layout.</param>
        public SectionElement(string tag, PersistenceLayout<T> layout)
        {
            this.Tag = tag;
            this.Layout = layout;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }
        /// <summary>
        /// Gets the layout.
        /// </summary>
        public PersistenceLayout<T> Layout { get; private set; }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public void Write(IFormatWriter writer, object value)
        {
            using (writer.Section(this.Tag))
            {
                this.Layout.Write(writer, value);
            }
        }
        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        public void Read(IFormatReader reader, object value)
        {
            using (reader.Section(this.Tag))
            {
                this.Layout.Read(reader, value);
            }
        }
        #endregion
    }
}
