using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Content.Layouts.Collections
{
    internal class CollectionElement<TElement> : CollectionBaseElement<TElement>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionElement{TElement}" /> class.
        /// </summary>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public CollectionElement(string elementTag, PropertyInfo property)
			: this(property.Name, elementTag, property)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public CollectionElement(string tag, string elementTag, PropertyInfo property)
			: base(tag, elementTag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
            if (!property.PropertyType.IsAssignableFrom(typeof(List<TElement>)))
            {
                throw new InvalidOperationException(
                    "The property " + property.Name + " is not assignable from List<" + typeof(TElement).Name + ">. " +
                    "Use DerivableScope.Collection to support collection inheritance.");
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public CollectionElement(string tag, string elementTag, Func<object, object> getAction, Action<object, object> setAction)
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
            if (element.GetType() != typeof(TElement))
            {
                throw new InvalidOperationException(
                    "The collection contains derived elements. " +
                    "Use the DerivableCollection method to serialize a collection containing derived elements.");
            }

            base.WriteElement(writer, element);
        }
        #endregion
    }
}
