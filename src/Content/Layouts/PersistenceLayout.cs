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
        /// Adds a property and references the registered serializer for the specified property type. NOTE: No inheritance possible.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The read action.</param>
        /// <param name="setAction">The write action.</param>
        public PersistenceLayout<T> Reference<TProperty>(string tag, Func<T, TProperty> getAction, Action<T, TProperty> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (TProperty)value);

            this.Add(new PropertyReferenceElement(tag, typeof(TProperty), read, write));
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
        /// Adds a property and references the registered serializer for the specified property value. NOTE: Inheritance possible due to typed serialization.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> DerivableReference<TProperty>(string tag, Func<T, TProperty> getAction, Action<T, TProperty> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (TProperty)value);

            this.Add(new DerivablePropertyReferenceElement(tag, read, write));
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
        /// Adds a collection to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Collection<TElement>(string tag, string elementTag, Func<T, ICollection<TElement>> getAction, Action<T, ICollection<TElement>> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (ICollection<TElement>)value);

            this.Add(new CollectionPropertyElement<TElement>(tag, elementTag, read, write));
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
        /// Adds a collection supporting derived elements to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> DerivableCollection<TElement>(string tag, string elementTag, Func<T, ICollection<TElement>> getAction, Action<T, ICollection<TElement>> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (ICollection<TElement>)value);

            this.Add(new DerivableCollectionPropertyElement<TElement>(tag, elementTag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, bool>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, bool>> property)
        {
            this.Add(new BooleanPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, bool> getAction, Action<T, bool> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (bool)value);

            this.Add(new BooleanPropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, byte>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, byte>> property)
        {
            this.Add(new BytePropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, byte> getAction, Action<T, byte> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (byte)value);

            this.Add(new BytePropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, char>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, char>> property)
        {
            this.Add(new CharPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, char> getAction, Action<T, char> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (char)value);

            this.Add(new CharPropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, double>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, double>> property)
        {
            this.Add(new DoublePropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, double> getAction, Action<T, double> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (double)value);

            this.Add(new DoublePropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, float>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, float>> property)
        {
            this.Add(new FloatPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, float> getAction, Action<T, float> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (float)value);

            this.Add(new FloatPropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, Guid>> property)
        {
            return this.Element("N", property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string guidFormat, Expression<Func<T, Guid>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, guidFormat, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, string guidFormat, Expression<Func<T, Guid>> property)
        {
            this.Add(new GuidPropertyElement(tag, guidFormat, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag,  Func<T, Guid> getAction, Action<T, Guid> setAction)
        {
            this.Element(tag, "N", getAction, setAction);
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, string guidFormat, Func<T, Guid> getAction, Action<T, Guid> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (Guid)value);

            this.Add(new GuidPropertyElement(tag, guidFormat, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, int>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, int>> property)
        {
            this.Add(new IntegerPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, int> getAction, Action<T, int> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (int)value);

            this.Add(new IntegerPropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, long>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, long>> property)
        {
            this.Add(new LongPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, long> getAction, Action<T, long> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (long)value);

            this.Add(new LongPropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, short>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, short>> property)
        {
            this.Add(new ShortPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, short> getAction, Action<T, short> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (short)value);

            this.Add(new ShortPropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, string>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, string>> property)
        {
            this.Add(new StringPropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, string> getAction, Action<T, string> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (string)value);

            this.Add(new StringPropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, Vector2>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, Vector2>> property)
        {
            this.Add(new Vector2PropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, Vector2> getAction, Action<T, Vector2> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (Vector2)value);

            this.Add(new Vector2PropertyElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(Expression<Func<T, Rectangle>> property)
        {
            return this.Element(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Element(string tag, Expression<Func<T, Rectangle>> property)
        {
            this.Add(new RectanglePropertyElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public PersistenceLayout<T> Element(string tag, Func<T, Rectangle> getAction, Action<T, Rectangle> setAction)
        {
            Func<object, object> read = obj => getAction((T)obj);
            Action<object, object> write = (container, value) => setAction((T)container, (Rectangle)value);

            this.Add(new RectanglePropertyElement(tag, read, write));
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
