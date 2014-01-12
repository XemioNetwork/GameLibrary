using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class PropertySectionElement<T, TProperty> : ILayoutElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySectionElement{T, TProperty}"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="layout">The layout.</param>
        public PropertySectionElement(string tag, Expression<Func<T, TProperty>> selector, PersistenceLayout<TProperty> layout)
        {
            this.Tag = tag;
            this.Selector = selector;
            this.Layout = layout;

            this._function = selector.Compile();
        }
        #endregion

        #region Fields
        private readonly Func<T, TProperty> _function; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }
        /// <summary>
        /// Gets the selector.
        /// </summary>
        public Expression<Func<T, TProperty>> Selector { get; private set; }
        /// <summary>
        /// Gets the layout.
        /// </summary>
        public PersistenceLayout<TProperty> Layout { get; private set; }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public void Write(IFormatWriter writer, object value)
        {
            using (writer.Section(this.Tag))
            {
                this.Layout.Write(writer, this._function((T)value));
            }
        }
        /// <summary>
        /// Reads a property value for the specified instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        public void Read(IFormatReader reader, object value)
        {
            using (reader.Section(this.Tag))
            {
                object propertyInstance = Activator.CreateInstance(typeof(TProperty), true);
                PropertyInfo property = PropertyHelper.GetProperty(this.Selector);

                this.Layout.Read(reader, propertyInstance);

                property.SetValue(value, propertyInstance);
            }
        }
        #endregion
    }
}
