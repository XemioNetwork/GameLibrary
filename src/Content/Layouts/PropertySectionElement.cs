using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class PropertySectionElement<T, TProperty> : ILayoutElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySectionElement{T, TProperty}"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="layout">The layout.</param>
        public PropertySectionElement(PropertyInfo property, InheritanceScope scope, PersistenceLayout<TProperty> layout) : this(property.Name, property, scope, layout)
        {
        }  
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySectionElement{T, TProperty}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        /// <param name="layout">The layout.</param>
        public PropertySectionElement(string tag, PropertyInfo property, InheritanceScope scope, PersistenceLayout<TProperty> layout)
        {
            this.Tag = tag;
            this.Property = property;
            this.Scope = scope;
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
        /// Gets the scope.
        /// </summary>
        public InheritanceScope Scope { get; private set; }
        /// <summary>
        /// Gets the layout.
        /// </summary>
        public PersistenceLayout<TProperty> Layout { get; private set; }
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
                object propertyInstance = this.Property.GetValue(container);
                if (this.Scope == InheritanceScope.Derived)
                {
                    writer.WriteString("Type", propertyInstance.GetType().AssemblyQualifiedName);
                }

                this.Layout.Write(writer, propertyInstance);
            }
        }
        /// <summary>
        /// Reads a property container for the specified instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public void Read(IFormatReader reader, object container)
        {
            using (reader.Section(this.Tag))
            {
                Type type = typeof(TProperty);
                if (this.Scope == InheritanceScope.Derived)
                {
                    type = Type.GetType(reader.ReadString("Type"));
                }

                object propertyInstance = Activator.CreateInstance(type, true);

                this.Layout.Read(reader, propertyInstance);
                this.Property.SetValue(container, propertyInstance);
            }
        }
        #endregion
    }
}
