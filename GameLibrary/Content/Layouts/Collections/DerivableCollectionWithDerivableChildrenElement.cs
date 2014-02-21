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
    internal class DerivableCollectionWithDerivableChildrenElement<TElement> : CollectionBaseElement<TElement>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DerivableCollectionWithDerivableChildrenElement{TElement}" /> class.
        /// </summary>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public DerivableCollectionWithDerivableChildrenElement(string elementTag, PropertyInfo property)
            : this(property.Name, elementTag, property)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DerivableCollectionWithDerivableChildrenElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public DerivableCollectionWithDerivableChildrenElement(string tag, string elementTag, PropertyInfo property)
            : base(tag, elementTag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DerivableCollectionWithDerivableChildrenElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public DerivableCollectionWithDerivableChildrenElement(string tag, string elementTag, Func<object, object> getAction, Action<object, object> setAction)
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
        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="length">The length.</param>
        protected override ICollection<TElement> ReadCollection(IFormatReader reader, int length)
        {
            string typeName = reader.ReadString("Type");
            Type type = Type.GetType(typeName);

            if (type.IsArray)
            {
                return new TElement[length];
            }

            return (ICollection<TElement>)Activator.CreateInstance(type, true);
        }
        /// <summary>
        /// Writes the collection.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="collection">The collection.</param>
        protected override void WriteCollection(IFormatWriter writer, ICollection<TElement> collection)
        {
            writer.WriteString("Type", collection.GetType().AssemblyQualifiedName);
        }
        #endregion
    }
}
