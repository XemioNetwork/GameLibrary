using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Layouts.Collections;
using Xemio.GameLibrary.Content.Layouts.Primitives;
using Xemio.GameLibrary.Content.Layouts.References;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class PersistenceLayout<T> : ILayoutElement, IElementContainer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceLayout{T}"/> class.
        /// </summary>
        public PersistenceLayout()
        {
            this._elements = new List<ILayoutElement>();
        }
        #endregion

        #region Fields
        private readonly IList<ILayoutElement> _elements;
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Add(ILayoutElement element)
        {
            this._elements.Add(element);
        }
        /// <summary>
        /// Adds a section to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="layout">The layout.</param>
        public PersistenceLayout<T> Section(string tag, Func<PersistenceLayout<T>, PersistenceLayout<T>> layout)
        {
            this.Add(new SectionElement(tag, layout(new PersistenceLayout<T>())));
            return this;
        }
        /// <summary>
        /// Adds a section to the persistence layout.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="layout">The layout.</param>
        public PersistenceLayout<T> Section<TProperty>(Expression<Func<T, TProperty>> property, Func<PersistenceLayout<TProperty>, PersistenceLayout<TProperty>> layout)
        {
            this.Add(new PropertySectionElement(PropertyHelper.GetProperty(property), layout(new PersistenceLayout<TProperty>())));
            return this;
        }
        /// <summary>
        /// Adds a section to the persistence layout.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        /// <param name="layout">The layout.</param>
        public PersistenceLayout<T> Section<TProperty>(string tag, Expression<Func<T, TProperty>> property, Func<PersistenceLayout<TProperty>, PersistenceLayout<TProperty>> layout)
        {
            this.Add(new PropertySectionElement(tag, PropertyHelper.GetProperty(property), layout(new PersistenceLayout<TProperty>())));
            return this;
        }
        /// <summary>
        /// Adds a property and references the registered serializer for the specified property type. NOTE: No inheritance possible.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Reference<TProperty>(string tag, Expression<Func<T, TProperty>> property)
        {
            this.Add(new PropertyReferenceElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property and references the registered serializer for the specified property value. NOTE: Inheritance possible due to typed serialization.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> DerivableReference<TProperty>(string tag, Expression<Func<T, TProperty>> property)
        {
            this.Add(new DerivablePropertyReferenceElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a collection to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Collection<TElement>(string elementTag, Expression<Func<T, ICollection<TElement>>> property)
        {
            this.Add(new CollectionPropertyElement<TElement>(elementTag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a collection to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Collection<TElement>(string tag, string elementTag, Expression<Func<T, ICollection<TElement>>> property)
        {
            this.Add(new CollectionPropertyElement<TElement>(tag, elementTag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a collection supporting derived elements to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> DerivableCollection<TElement>(string elementTag, Expression<Func<T, ICollection<TElement>>> property)
        {
            this.Add(new DerivableCollectionPropertyElement<TElement>(elementTag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a collection supporting derived elements to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> DerivableCollection<TElement>(string tag, string elementTag, Expression<Func<T, ICollection<TElement>>> property)
        {
            this.Add(new DerivableCollectionPropertyElement<TElement>(tag, elementTag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, bool>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, bool>> property)
        {
            this.Add(new BooleanPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, byte>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, byte>> property)
        {
            this.Add(new BytePropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, char>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, char>> property)
        {
            this.Add(new CharPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, double>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, double>> property)
        {
            this.Add(new DoublePropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, float>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, float>> property)
        {
            this.Add(new FloatPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, Guid>> property)
        {
            return this.Property("N", property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string guidFormat, Expression<Func<T, Guid>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, guidFormat, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, string guidFormat, Expression<Func<T, Guid>> property)
        {
            this.Add(new GuidPropertyElement(tag, guidFormat, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, int>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, int>> property)
        {
            this.Add(new IntegerPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, long>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, long>> property)
        {
            this.Add(new LongPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, short>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, short>> property)
        {
            this.Add(new ShortPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, string>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, string>> property)
        {
            this.Add(new StringPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, Vector2>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, Vector2>> property)
        {
            this.Add(new Vector2PropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(Expression<Func<T, Rectangle>> property)
        {
            return this.Property(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Property(string tag, Expression<Func<T, Rectangle>> property)
        {
            this.Add(new RectanglePropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public void Write(IFormatWriter writer, object container)
        {
            foreach (ILayoutElement element in this._elements)
            {
                element.Write(writer, container);
            }
        }
        /// <summary>
        /// Reads properties for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public void Read(IFormatReader reader, object container)
        {
            foreach (ILayoutElement element in this._elements)
            {
                element.Read(reader, container);
            }
        }
        #endregion
    }
}
