using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public static class ReflectionCache
    {
        #region Static Fields
        private static readonly Dictionary<object, Dictionary<string, object>> _cache = new Dictionary<object, Dictionary<string, object>>();
        #endregion

        #region Methods
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="name">The name.</param>
        /// <param name="computation">The computation.</param>
        private static T Get<T>(object key, string name, Func<T> computation)
        {
            if (!_cache.ContainsKey(key))
                _cache.Add(key, new Dictionary<string, object>());

            if (!_cache[key].ContainsKey(name))
                _cache[key].Add(name, computation());

            return (T)_cache[key][name];
        }
        /// <summary>
        /// Gets the interfaces for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public static Type[] GetInterfaces(Type type)
        {
            return ReflectionCache.Get(type, "Interfaces", type.GetInterfaces);
        }
        /// <summary>
        /// Gets the properties for the specified type.
        /// </summary>
        /// <param name="type">The key.</param>
        public static PropertyInfo[] GetProperties(Type type)
        {
            return ReflectionCache.Get(type, "Properties", () => type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
        }
        /// <summary>
        /// Gets custom attributes for the specified property.
        /// </summary>
        /// <param name="type">The type.</param>
        public static object[] GetCustomAttributes(Type type)
        {
            return ReflectionCache.Get(type, "CustomAttributes", () => type.GetCustomAttributes(true));
        }
        /// <summary>
        /// Gets custom attributes for the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public static object[] GetCustomAttributes(PropertyInfo propertyInfo)
        {
            return ReflectionCache.Get(propertyInfo, "CustomAttributes", () => propertyInfo.GetCustomAttributes(true));
        }
        /// <summary>
        /// Determines whether the specified type has a custom attribute with the specified attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="type">The type.</param>
        public static bool HasCustomAttribute<TAttribute>(Type type)
        {
            return ReflectionCache.GetCustomAttributes(type).Any(attr => attr is TAttribute);
        }
        /// <summary>
        /// Determines whether the specified type has a custom attribute with the specified attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="propertyInfo">The property information.</param>
        public static bool HasCustomAttribute<TAttribute>(PropertyInfo propertyInfo)
        {
            return ReflectionCache.GetCustomAttributes(propertyInfo).Any(attr => attr is TAttribute);
        }
        /// <summary>
        /// Gets the constructors for the specified key.
        /// </summary>
        /// <param name="type">The key.</param>
        public static ConstructorInfo[] GetConstructors(Type type)
        {
            return ReflectionCache.Get(type, "Constructors", type.GetConstructors);
        }
        /// <summary>
        /// Gets the generic arguments.
        /// </summary>
        /// <param name="type">The key.</param>
        public static Type[] GetGenericArguments(Type type)
        {
            return ReflectionCache.Get(type, "GenericArguments", type.GetGenericArguments);
        }
        /// <summary>
        /// Gets the element key for the specified key.
        /// </summary>
        /// <param name="type">The key.</param>
        public static Type GetElementType(Type type)
        {
            return ReflectionCache.Get(type, "ElementType", type.GetElementType);
        }
        /// <summary>
        /// Gets the constructor parameters for the specified constructor.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        public static ParameterInfo[] GetConstructorParameters(ConstructorInfo constructor)
        {
            return ReflectionCache.Get(constructor, "Parameters", constructor.GetParameters);
        }
        /// <summary>
        /// Gets the set method.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public static MethodInfo GetSetMethod(PropertyInfo propertyInfo)
        {
            return ReflectionCache.Get(propertyInfo, "SetMethod", propertyInfo.GetSetMethod);
        }
        /// <summary>
        /// Gets the get method.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public static MethodInfo GetGetMethod(PropertyInfo propertyInfo)
        {
            return ReflectionCache.Get(propertyInfo, "GetMethod", propertyInfo.GetGetMethod);
        }
        #endregion
    }
}
