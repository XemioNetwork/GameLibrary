using System;
using System.Collections.Generic;
using System.Reflection;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Collections
{
    internal class CollectionPropertyElement<TElement> : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionPropertyElement{TElement}" /> class.
        /// </summary>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public CollectionPropertyElement(string elementTag, PropertyInfo property) : this(property.Name, elementTag, property)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionPropertyElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public CollectionPropertyElement(string tag, string elementTag, PropertyInfo property) : this(tag, elementTag, property.GetValue, property.SetValue)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionPropertyElement{TElement}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public CollectionPropertyElement(string tag, string elementTag, Func<object, object> getAction, Action<object, object> setAction) : base(tag, getAction, setAction)
        {
            this.ElementTag = elementTag;
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the element tag.
        /// </summary>
        public string ElementTag { get; private set; }
        /// <summary>
        /// Gets the serializer.
        /// </summary>
        protected SerializationManager Serializer
        {
            get { return XGL.Components.Require<SerializationManager>(); }
        }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes partial data for the container into the specified format.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public override void Write(IFormatWriter writer, object container)
        {
            using (writer.Section(this.Tag))
            {
                var collection = (ICollection<TElement>)this.GetAction(container);

                writer.WriteInteger("Length", collection.Count);
                foreach (TElement element in collection)
                {
                    using (writer.Section(this.ElementTag))
                    {
                        if (element.GetType() != typeof(TElement))
                        {
                            throw new InvalidOperationException(
                                "The collection contains derived elements. " +
                                "Use the DerivableCollection method to serialize a collection containing derived elements.");
                        }

                        this.Serializer.Save(element, writer);
                    }
                }
            }
        }
        /// <summary>
        /// Reads partial data for the container from the specified format.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            using (reader.Section(this.Tag))
            {
                ICollection<TElement> collection = new List<TElement>();
                int length = reader.ReadInteger("Length");

                for (int i = 0; i < length; i++)
                {
                    using (reader.Section(this.ElementTag))
                    {
                        collection.Add(this.Serializer.Load<TElement>(reader));
                    }
                }

                this.SetAction(container, collection);
            }
        }
        #endregion
    }
}
