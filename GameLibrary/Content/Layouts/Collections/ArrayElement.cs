using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Content.Layouts.Collections
{
    public class ArrayElement<T> : CollectionBaseElement<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionElement{TElement}" /> class.
        /// </summary>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public ArrayElement(string elementTag, PropertyInfo property)
			: this(property.Name, elementTag, property)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public ArrayElement(string tag, string elementTag, PropertyInfo property)
            : base(tag, elementTag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public ArrayElement(string tag, string elementTag, Func<object, object> getAction, Action<object, object> setAction)
            : base(tag, elementTag, getAction, setAction)
        {
        }
        #endregion

        #region Overrides of CollectionBaseElement<T>
        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="length">The length.</param>
        protected override ICollection<T> ReadCollection(IFormatReader reader, int length)
        {
            return new T[length];
        }
        #endregion
    }
}
