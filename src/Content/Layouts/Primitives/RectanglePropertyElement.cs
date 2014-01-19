﻿using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class RectanglePropertyElement : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RectanglePropertyElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public RectanglePropertyElement(string tag, PropertyInfo property) : this(tag, property.GetValue, property.SetValue)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RectanglePropertyElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public RectanglePropertyElement(string tag, Func<object, object> getAction, Action<object, object> setAction) : base(tag, getAction, setAction)
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
            writer.WriteRectangle(this.Tag, (Rectangle)this.GetAction(container));
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            this.SetAction(container, reader.ReadRectangle(this.Tag));
        }
        #endregion
    }
}
