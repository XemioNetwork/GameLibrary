using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Primitives
{
    internal class TypeElement : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public TypeElement(string tag, PropertyInfo property) 
            : this(tag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public TypeElement(string tag, Func<object, object> getAction, Action<object, object> setAction) 
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
            var type = (Type)this.GetAction(container);
            string fullName = type.AssemblyQualifiedName;

            writer.WriteString(this.Tag, fullName);
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            string fullName = reader.ReadString(this.Tag);
            Type type = Type.GetType(fullName);

            this.SetAction(container, type);
        }
        #endregion
    }
}
