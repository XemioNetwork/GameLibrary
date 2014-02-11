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

        #region Protected Methods
        /// <summary>
        /// Creates the get action.
        /// </summary>
        /// <typeparam name="TContainer">The type of the container.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        protected virtual Func<object, object> CreateGetAction<TContainer, TProperty>(string tag, Func<TContainer, TProperty> getAction)
        {
            return obj => getAction((TContainer)obj);
        }
        /// <summary>
        /// Creates the set action.
        /// </summary>
        /// <typeparam name="TContainer">The type of the container.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="setAction">The set action.</param>
        protected virtual Action<object, object> CreateSetAction<TContainer, TProperty>(string tag,Action<TContainer, TProperty> setAction)
        {
            return (container, value) => setAction((TContainer)container, (TProperty)value);
        } 
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
            this.Add(new ReferenceElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new ReferenceElement(tag, typeof(TProperty), read, write));
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
            this.Add(new DerivableReferenceElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new DerivableReferenceElement(tag, read, write));
            return this;
        }
        /// <summary>
        /// Adds a collection to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="property">The property.</param>
        /// <param name="scope">The scope.</param>
        public PersistenceLayout<T> Collection<TElement>(string elementTag, Expression<Func<T, ICollection<TElement>>> property, DerivableScope scope = DerivableScope.None)
        {
            if (scope.HasFlag(DerivableScope.Element) && scope.HasFlag(DerivableScope.Collection))
            {
                this.Add(new DerivableCollectionWithDerivableChildrenElement<TElement>(elementTag, PropertyHelper.GetProperty(property)));
            }
            else if (scope.HasFlag(DerivableScope.Element))
            {
                this.Add(new CollectionWithDerivableChildrenElement<TElement>(elementTag, PropertyHelper.GetProperty(property)));
            }
            else if (scope.HasFlag(DerivableScope.Collection))
            {
                this.Add(new DerivableCollectionElement<TElement>(elementTag, PropertyHelper.GetProperty(property)));
            }
            else
            {
                this.Add(new CollectionElement<TElement>(elementTag, PropertyHelper.GetProperty(property)));
            }

            return this;
        }
        /// <summary>
        /// Adds a collection to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        /// <param name="scope">The scope.</param>
        public PersistenceLayout<T> Collection<TElement>(string elementTag, string tag, Expression<Func<T, ICollection<TElement>>> property, DerivableScope scope = DerivableScope.None)
        {
            if (scope.HasFlag(DerivableScope.Element) && scope.HasFlag(DerivableScope.Collection))
            {
                this.Add(new DerivableCollectionWithDerivableChildrenElement<TElement>(tag, elementTag, PropertyHelper.GetProperty(property)));
            }
            else if (scope.HasFlag(DerivableScope.Element))
            {
                this.Add(new CollectionWithDerivableChildrenElement<TElement>(tag, elementTag, PropertyHelper.GetProperty(property)));
            }
            else if (scope.HasFlag(DerivableScope.Collection))
            {
                this.Add(new DerivableCollectionElement<TElement>(tag, elementTag, PropertyHelper.GetProperty(property)));
            }
            else
            {
                this.Add(new CollectionElement<TElement>(tag, elementTag, PropertyHelper.GetProperty(property)));
            }

            return this;
        }
        /// <summary>
        /// Adds a collection to the persistence layout.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        /// <param name="scope">The scope.</param>
        public PersistenceLayout<T> Collection<TElement>(string elementTag, string tag, Func<T, ICollection<TElement>> getAction, Action<T, ICollection<TElement>> setAction, DerivableScope scope = DerivableScope.None)
        {
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            if (scope.HasFlag(DerivableScope.Element) && scope.HasFlag(DerivableScope.Collection))
            {
                this.Add(new DerivableCollectionWithDerivableChildrenElement<TElement>(tag, elementTag, read, write));
            }
            else if (scope.HasFlag(DerivableScope.Element))
            {
                this.Add(new CollectionWithDerivableChildrenElement<TElement>(tag, elementTag, read, write));
            }
            else if (scope.HasFlag(DerivableScope.Collection))
            {
                this.Add(new DerivableCollectionElement<TElement>(tag, elementTag, read, write));
            }
            else
            {
                this.Add(new CollectionElement<TElement>(tag, elementTag, read, write));
            }

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
            this.Add(new BooleanElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new BooleanElement(tag, read, write));
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
            this.Add(new ByteElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new ByteElement(tag, read, write));
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
            this.Add(new CharElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new CharElement(tag, read, write));
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
            this.Add(new DoubleElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new DoubleElement(tag, read, write));
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
            this.Add(new FloatElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new FloatElement(tag, read, write));
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
            this.Add(new GuidElement(tag, guidFormat, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new GuidElement(tag, guidFormat, read, write));
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
            this.Add(new IntegerElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new IntegerElement(tag, read, write));
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
            this.Add(new LongElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new LongElement(tag, read, write));
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
            this.Add(new ShortElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new ShortElement(tag, read, write));
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
            this.Add(new StringElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new StringElement(tag, read, write));
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
            this.Add(new Vector2Element(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new Vector2Element(tag, read, write));
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
            this.Add(new RectangleElement(tag, PropertyHelper.GetProperty(property)));
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
            Func<object, object> read = this.CreateGetAction(tag, getAction);
            Action<object, object> write = this.CreateSetAction(tag, setAction);

            this.Add(new RectangleElement(tag, read, write));
            return this;
        }
        #endregion

        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public virtual void Write(IFormatWriter writer, object container)
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
        public virtual void Read(IFormatReader reader, object container)
        {
            foreach (ILayoutElement element in this._elements)
            {
                element.Read(reader, container);
            }
        }
        #endregion
    }
}
