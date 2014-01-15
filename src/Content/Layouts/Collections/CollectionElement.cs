using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Collections
{
    public class CollectionElement<TItem> : ILayoutElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionElement{TItem}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        /// <param name="layout">The layout.</param>
        public CollectionElement(string tag, PropertyInfo property, PersistenceLayout<TItem> layout)
        {
            this.Tag = tag;
            this.Property = property;
            this.Layout = layout;
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }
        /// <summary>
        /// Gets the property.
        /// </summary>
        public PropertyInfo Property { get; private set; }
        /// <summary>
        /// Gets the layout.
        /// </summary>
        public PersistenceLayout<TItem> Layout { get; private set; }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public void Write(IFormatWriter writer, object container)
        {
            using (writer.Section(this.Tag))
            {
                var collection = (ICollection<TItem>)this.Property.GetValue(container);

                writer.WriteInteger("Length", collection.Count);
                foreach (TItem item in collection)
                {
                    this.Layout.Write(writer, item);
                }
            }
        }
        /// <summary>
        /// Reads a property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public void Read(IFormatReader reader, object container)
        {
            using (reader.Section(this.Tag))
            {
                int length = reader.ReadInteger("Length");
                Type type = typeof(List<TItem>);
                
                var collection = (ICollection<TItem>)Activator.CreateInstance(type, true);
                for (int i = 0; i < length; i++)
                {

                }
            }
        }
        #endregion
    }
}
