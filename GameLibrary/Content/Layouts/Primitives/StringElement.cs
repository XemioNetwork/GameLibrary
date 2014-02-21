using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class StringElement : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public StringElement(string tag, PropertyInfo property) 
			: this(tag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StringElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public StringElement(string tag, Func<object, object> getAction, Action<object, object> setAction) 
			: base(tag, getAction, setAction)
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
            writer.WriteString(this.Tag, (string)this.GetAction(container));
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            this.SetAction(container, reader.ReadString(this.Tag));
        }
        #endregion
    }
}
