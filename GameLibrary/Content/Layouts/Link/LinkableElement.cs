using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Content.Layouts.Link
{
    internal class LinkableElement<TKey, TValue> : BaseElement where TValue : ILinkable<TKey>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkableElement{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public LinkableElement(PropertyInfo property)
			: this(property.Name, property)
        {
        } 
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkableElement{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public LinkableElement(string tag, PropertyInfo property)
			: this(tag, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkableElement{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public LinkableElement(string tag, Func<object, object> getAction, Action<object, object> setAction)
			: base(tag, getAction, setAction)
        {
        }
        #endregion

        #region Overrides of BaseElement
        /// <summary>
        /// Writes property for the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public override void Write(IFormatWriter writer, object container)
        {
            object linkable = this.GetAction(container);
            var id = (TKey)Reflection.GetProperties(linkable.GetType())
                                .First(p => p.Name == "Id")
                                .GetValue(linkable, null);

            var serializer = XGL.Components.Require<SerializationManager>();
            serializer.Save(id, writer);
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            var implementations = XGL.Components.Require<IImplementationManager>();
            var serializer = XGL.Components.Require<SerializationManager>();

            var id = serializer.Load<TKey>(reader);

            this.SetAction(container, implementations.Get<TKey, TValue>(id));
        }
        #endregion
    }
}
