﻿using System.Reflection;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class StringPropertyElement : PropertyElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringPropertyElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public StringPropertyElement(string tag, PropertyInfo property) : base(tag, property)
        {
        }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes property for the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public override void Write(IFormatWriter writer, object container)
        {
            writer.WriteString(this.Tag, (string)this.Property.GetValue(container));
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            this.Property.SetValue(container, reader.ReadString(this.Tag));
        }
        #endregion
    }
}