using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class PersistenceLayout<T> : ILayoutElement
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
        /// Adds a section to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="layout">The layout.</param>
        public PersistenceLayout<T> Section(string tag, Func<PersistenceLayout<T>, PersistenceLayout<T>> layout)
        {
            this._elements.Add(new SectionElement<T>(tag, layout(new PersistenceLayout<T>())));
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
            return this.Section(PropertyHelper.GetProperty(property).Name, property, layout);
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
            this._elements.Add(new PropertySectionElement<T, TProperty>(tag, property, layout(new PersistenceLayout<TProperty>())));
            return this;
        }
        /// <summary>
        /// Adds a collection layout.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="layout">The layout.</param>
        public PersistenceLayout<T> Each<TItem>(string tag, Expression<Func<T, IEnumerable<TItem>>> collection, Func<PersistenceLayout<TItem>, PersistenceLayout<TItem>> layout)
        {
            
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Boolean(Expression<Func<T, bool>> property)
        {
            return this.Boolean(PropertyHelper.GetProperty(property).Name, property);
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Boolean(string tag, Expression<Func<T, bool>> property)
        {
            this._elements.Add(new BooleanElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Byte(string tag, Expression<Func<T, byte>> property)
        {
            this._elements.Add(new ByteElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Char(string tag, Expression<Func<T, char>> property)
        {
            this._elements.Add(new CharElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Double(string tag, Expression<Func<T, double>> property)
        {
            this._elements.Add(new DoubleElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Float(string tag, Expression<Func<T, float>> property)
        {
            this._elements.Add(new FloatElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guidFormat">The unique identifier format.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Guid(string tag, string guidFormat, Expression<Func<T, Guid>> property)
        {
            this._elements.Add(new GuidElement(tag, guidFormat, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Integer(string tag, Expression<Func<T, int>> property)
        {
            this._elements.Add(new IntegerElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Long(string tag, Expression<Func<T, long>> property)
        {
            this._elements.Add(new LongElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Short(string tag, Expression<Func<T, short>> property)
        {
            this._elements.Add(new ShortElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> String(string tag, Expression<Func<T, string>> property)
        {
            this._elements.Add(new StringElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Vector2(string tag, Expression<Func<T, Vector2>> property)
        {
            this._elements.Add(new Vector2Element(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        /// <summary>
        /// Adds a property to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public PersistenceLayout<T> Rectangle(string tag, Expression<Func<T, Rectangle>> property)
        {
            this._elements.Add(new RectangleElement(tag, PropertyHelper.GetProperty(property)));
            return this;
        }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public void Write(IFormatWriter writer, object value)
        {
            foreach (ILayoutElement element in this._elements)
            {
                element.Write(writer, value);
            }
        }
        /// <summary>
        /// Reads properties for the specified value.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        public void Read(IFormatReader reader, object value)
        {
            foreach (ILayoutElement element in this._elements)
            {
                element.Read(reader, value);
            }
        }
        #endregion
    }
}
