using System;
using System.Collections.Generic;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Collections
{
    internal class CollectionWithDerivableChildrenElement<TElement> : CollectionBaseElement<TElement>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionWithDerivableChildrenElement{TElement}" /> class.
        /// </summary>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public CollectionWithDerivableChildrenElement(string elementTag, PropertyInfo property) : this(property.Name, elementTag, property)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionWithDerivableChildrenElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public CollectionWithDerivableChildrenElement(string tag, string elementTag, PropertyInfo property) : base(tag, elementTag, property.GetValue, property.SetValue)
        {
            if (!property.PropertyType.IsAssignableFrom(typeof(List<TElement>)))
            {
                throw new InvalidOperationException(
                    "The property " + property.Name + " is not assignable from List<" + typeof(TElement).Name + ">. " +
                    "Use DerivableScope.Collection to support collection inheritance.");
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionWithDerivableChildrenElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public CollectionWithDerivableChildrenElement(string tag, string elementTag, Func<object, object> getAction, Action<object, object> setAction)
             : base(tag, elementTag, getAction, setAction)
        {
        }
        #endregion

        #region Overrides of CollectionBaseElement<TElement>
        /// <summary>
        /// Writes an element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="element">The element.</param>
        protected override void WriteElement(IFormatWriter writer, TElement element)
        {
            writer.WriteString("Type", element.GetType().AssemblyQualifiedName);
            this.Serializer.Save(element, writer);
        }
        /// <summary>
        /// Reads an element.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override TElement ReadElement(IFormatReader reader)
        {
            string typeName = reader.ReadString("Type");
            Type type = Type.GetType(typeName);

            return (TElement)this.Serializer.Load(type, reader);
        }
        #endregion
    }
}
