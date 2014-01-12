using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public abstract class PropertyElement : ILayoutElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyElement"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        protected PropertyElement(string tag, PropertyInfo property)
        {
            this.Tag = tag;
            this.Property = property;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        protected string Tag { get; private set; }
        /// <summary>
        /// Gets the property.
        /// </summary>
        protected PropertyInfo Property { get; private set; }
        #endregion
        
        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes property for the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public abstract void Write(IFormatWriter writer, object value);
        /// <summary>
        /// Reads the property for the specified value.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        public abstract void Read(IFormatReader reader, object value);
        #endregion
    }
}
