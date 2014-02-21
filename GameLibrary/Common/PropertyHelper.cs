using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Xemio.GameLibrary.Common
{
    public static class PropertyHelper
    {
        #region Methods
        /// <summary>
        /// Gets the property info for the specified expression.
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public static PropertyInfo GetProperty<TInstance, TProperty>(Expression<Func<TInstance, TProperty>> expression)
        {
            Type type = typeof(TProperty);

            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", expression));
            }

            var property = member.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", expression));
            }

            if (!type.IsAssignableFrom(property.PropertyType))
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.", expression, type));
            }

            return property;
        }
        /// <summary>
        /// Gets the get method for the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
		public static Func<object, object> Get(PropertyInfo property)
		{
			return instance => property.GetValue(instance, null);
		}
        /// <summary>
        /// Gets the set method for the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
		public static Action<object, object> Set(PropertyInfo property) 
		{
			return (instance, value) => property.SetValue(instance, value, null);
		}
        #endregion
    }
}
