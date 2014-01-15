using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Layouts.Primitives;
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
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public PersistenceLayout<T> Add(ILayoutElement element)
        {
            this._elements.Add(element);
            return this;
        }
        /// <summary>
        /// Adds a section to the persistence layout.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public CascadedSectionPersistenceLayout<T> Section(string tag)
        {
            return new CascadedSectionPersistenceLayout<T>(this, tag);
        }
        /// <summary>
        /// Adds a section to the persistence layout.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        public CascadedPropertyPersistenceLayout<T, TProperty> Section<TProperty>(Expression<Func<T, TProperty>> property)
        {
            return new CascadedPropertyPersistenceLayout<T, TProperty>(this, PropertyHelper.GetProperty(property));
        }
        /// <summary>
        /// Adds a section to the persistence layout.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public CascadedPropertyPersistenceLayout<T, TProperty> Section<TProperty>(string tag, Expression<Func<T, TProperty>> property)
        {
            return new CascadedPropertyPersistenceLayout<T, TProperty>(this, tag, PropertyHelper.GetProperty(property));
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
