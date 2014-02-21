using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class CharElement : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CharElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public CharElement(string tag, PropertyInfo property)
			: this(tag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CharElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public CharElement(string tag, Func<object, object> getAction, Action<object, object> setAction)
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
            writer.WriteCharacter(this.Tag, (char)this.GetAction(container));
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            this.SetAction(container, reader.ReadCharacter(this.Tag));
        }
        #endregion
    }
}
