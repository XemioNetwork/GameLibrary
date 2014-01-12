﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class StringElement : PropertyElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public StringElement(string tag, PropertyInfo property) : base(tag, property)
        {
        }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes property for the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, object value)
        {
            writer.WriteString(this.Tag, (string)this.Property.GetValue(value));
        }
        /// <summary>
        /// Reads the property for the specified value.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        public override void Read(IFormatReader reader, object value)
        {
            this.Property.SetValue(value, reader.ReadString(this.Tag));
        }
        #endregion
    }
}