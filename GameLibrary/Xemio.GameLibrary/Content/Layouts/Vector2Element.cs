﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class Vector2Element : PropertyElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2Element" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public Vector2Element(string tag, PropertyInfo property) : base(tag, property)
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
            writer.WriteVector2(this.Tag, (Vector2)this.Property.GetValue(value));
        }
        /// <summary>
        /// Reads the property for the specified value.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        public override void Read(IFormatReader reader, object value)
        {
            this.Property.SetValue(value, reader.ReadVector2(this.Tag));
        }
        #endregion
    }
}