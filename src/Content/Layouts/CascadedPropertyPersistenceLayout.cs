using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class CascadedPropertyPersistenceLayout<TParent, T> : PersistenceLayout<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CascadedPropertyPersistenceLayout{TParent,T}" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="property">The property.</param>
        public CascadedPropertyPersistenceLayout(PersistenceLayout<TParent> parent, PropertyInfo property) : this(parent, property.Name, property)
        {
        }  
        /// <summary>
        /// Initializes a new instance of the <see cref="CascadedPropertyPersistenceLayout{TParent,T}" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public CascadedPropertyPersistenceLayout(PersistenceLayout<TParent> parent, string tag, PropertyInfo property)
        {
            this._parent = parent;

            this._tag = tag;
            this._property = property;
        }  
        #endregion

        #region Fields
        private readonly PersistenceLayout<TParent> _parent;

        private readonly string _tag;
        private readonly PropertyInfo _property;
        #endregion

        #region Methods
        /// <summary>
        /// Ends this layout.
        /// </summary>
        public PersistenceLayout<TParent> End()
        {
            return this._parent.Add(new PropertySectionElement<TParent, T>(this._tag, this._property, InheritanceScope.Static, this));
        } 
        #endregion
    }
}
