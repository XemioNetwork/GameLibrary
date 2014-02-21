using System;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class Vector2Element : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2Element" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public Vector2Element(string tag, PropertyInfo property) 
            : this(tag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2Element" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public Vector2Element(string tag, Func<object, object> getAction, Action<object, object> setAction) 
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
            writer.WriteVector2(this.Tag, (Vector2)this.GetAction(container));
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            this.SetAction(container, reader.ReadVector2(this.Tag));
        }
        #endregion
    }
}
