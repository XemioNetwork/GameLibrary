using System;
using System.Linq.Expressions;
using System.Reflection;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.DataBindings
{
    public class DataBinder
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DataBinder"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public DataBinder(Widget widget)
        {
            this._propertyBinder = new PropertyBinder();
            this.Widget = widget;
        }
        #endregion

        #region Fields
        private readonly PropertyBinder _propertyBinder;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the widget.
        /// </summary>
        public Widget Widget { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Converts a linq expression to a property.
        /// </summary>
        /// <param name="property">The property.</param>
        private Property GetProperty<T>(Expression<Func<T>> property)
        {
            MemberExpression expression = property.Body as MemberExpression;
            PropertyInfo info = expression.Member as PropertyInfo;

            Delegate compiledExpression = Expression.Lambda(expression.Expression).Compile();
            object instance = compiledExpression.DynamicInvoke();
            
            return new Property(instance, info);
        }
        /// <summary>
        /// Binds the specified property to the destination property.
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <param name="property">The source property.</param>
        /// <param name="destination">The destination.</param>
        public void Bind<T>(Widget widget, Expression<Func<T>> property, Expression<Func<T>> destination)
        {
            Property p = this.GetProperty<T>(property);
            Property d = this.GetProperty<T>(destination);

            if (p.Instance != widget)
            {
                throw new ArgumentException(
                    "The first property has to be a member of the defined widget",
                    "property");
            }

            this._propertyBinder.Register(p, d);
        }
        /// <summary>
        /// Updates the bindings.
        /// </summary>
        public void UpdateBindings()
        {
            this._propertyBinder.UpdateBindings();
        }
        #endregion
    }
}
