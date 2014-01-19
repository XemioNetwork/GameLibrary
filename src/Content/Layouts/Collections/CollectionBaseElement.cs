using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Collections
{
    internal class CollectionBaseElement<TElement> : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public CollectionBaseElement(string tag, string elementTag, Func<object, object> getAction, Action<object, object> setAction) : base(tag, getAction, setAction)
        {
            this.ElementTag = elementTag;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the element tag.
        /// </summary>
        public string ElementTag { get; private set; }
        #endregion

        #region Serializer
        /// <summary>
        /// Gets the serializer.
        /// </summary>
        protected SerializationManager Serializer
        {
            get { return XGL.Components.Require<SerializationManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="length">The length.</param>
        protected virtual ICollection<TElement> ReadCollection(IFormatReader reader, int length)
        {
            return new List<TElement>(length);
        }
        /// <summary>
        /// Writes the collection.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="collection">The collection.</param>
        protected virtual void WriteCollection(IFormatWriter writer, ICollection<TElement> collection)
        {
        }
        /// <summary>
        /// Writes an element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="element">The element.</param>
        protected virtual void WriteElement(IFormatWriter writer, TElement element)
        {
            writer.WriteString("Type", element.GetType().AssemblyQualifiedName);
            this.Serializer.Save(element, writer);
        }
        /// <summary>
        /// Reads an element.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected virtual TElement ReadElement(IFormatReader reader)
        {
            return this.Serializer.Load<TElement>(reader);
        }
        #endregion

        #region Overrides of BaseElement
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
                this.WriteCollection(writer, collection);

                foreach (TElement element in collection)
                {
                    using (writer.Section(this.ElementTag))
                    {
                        this.WriteElement(writer, element);
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
                int length = reader.ReadInteger("Length");

                ICollection<TElement> collection = this.ReadCollection(reader, length);
                var array = collection as TElement[];

                for (int i = 0; i < length; i++)
                {
                    using (reader.Section(this.ElementTag))
                    {
                        TElement element = this.ReadElement(reader);
                        if (array != null)
                        {
                            array[i] = element;
                        }
                        else
                        {
                            collection.Add(element);
                        }
                    }
                }

                this.SetAction(container, collection);
            }
        }
        #endregion
    }
}
