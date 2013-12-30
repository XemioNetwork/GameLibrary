using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Attributes;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization.Automatic.Processors
{
    public class PropertyProcessor : IAutomaticProcessor
    {
        #region Implementation of IAutomaticProcessor
        /// <summary>
        /// Gets the priority.
        /// </summary>
        public int Priority
        {
            get { return 0; }
        }
        /// <summary>
        /// Determines whether this instance can process the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public bool CanProcess(Type type)
        {
            return true;
        }
        /// <summary>
        /// Reads an instance of the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        public object Read(IFormatReader reader, Type type)
        {
            var serializer = XGL.Components.Require<SerializationManager>();
            var propertyValues = new Dictionary<PropertyInfo, object>();

            foreach (PropertyInfo property in ReflectionCache.GetProperties(type))
            {
                if (!ReflectionCache.HasCustomAttribute<ExcludeSerializationAttribute>(property))
                {
                    using (reader.Section(property.Name))
                    {
                        //FIXME: clean up code
                        bool isValueType = property.PropertyType.IsValueType;
                        bool isNull = (isValueType == false && reader.ReadBoolean("IsNull"));

                        Type propertyType = !isValueType && !isNull
                            ? TypeHelper.ReadType(property.PropertyType, reader)
                            : property.PropertyType;

                        propertyValues.Add(property, isNull ? null : serializer.Load(propertyType, reader));
                    }
                }
            }

            object instance = this.InvokeConstructor(type, propertyValues);

            foreach (KeyValuePair<PropertyInfo, object> pair in propertyValues)
            {
                if (ReflectionCache.GetSetMethod(pair.Key) != null)
                {
                    pair.Key.SetValue(instance, pair.Value, null);
                }
            }

            return instance;
        }
        /// <summary>
        /// Writes the specified instance.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="instance">The instance.</param>
        public void Write(IFormatWriter writer, object instance)
        {
            var serializer = XGL.Components.Require<SerializationManager>();
            PropertyInfo[] properties = ReflectionCache.GetProperties(instance.GetType());

            foreach (PropertyInfo property in properties)
            {
                if (!ReflectionCache.HasCustomAttribute<ExcludeSerializationAttribute>(property))
                {
                    using (writer.Section(property.Name))
                    {
                        object propertyValue = property.GetValue(instance, null);
                        bool isNull = (propertyValue == null);

                        if (property.PropertyType.IsValueType == false)
                        {
                            writer.WriteBoolean("IsNull", isNull);
                            if (isNull == false)
                            {
                                TypeHelper.WriteType(propertyValue.GetType(), property.PropertyType, writer);
                            }
                        }

                        if (isNull == false)
                        {
                            serializer.Save(propertyValue, writer);
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the instance with constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="properties">The properties.</param>
        private object InvokeConstructor(Type type, IEnumerable<KeyValuePair<PropertyInfo, object>> properties)
        {
            PropertyInfo[] propertyInfos = ReflectionCache.GetProperties(type);

            foreach (ConstructorInfo constructor in ReflectionCache.GetConstructors(type))
            {
                List<ParameterInfo> parameters = ReflectionCache.GetConstructorParameters(constructor).ToList();

                bool hasConstructorParameterForEveryProperty =
                    parameters.All(parameter => propertyInfos.Any(property => property.Name.ToLower() == parameter.Name.ToLower()));

                if (hasConstructorParameterForEveryProperty)
                {
                    var constructorParameters = new object[parameters.Count];

                    foreach (KeyValuePair<PropertyInfo, object> pair in properties)
                    {
                        ParameterInfo parameter = parameters.FirstOrDefault(p => p.Name.ToLower() == pair.Key.Name.ToLower());

                        if (parameter != null)
                        {
                            int index = parameters.IndexOf(parameter);
                            constructorParameters[index] = pair.Value;
                        }
                    }

                    return constructor.Invoke(constructorParameters);
                }
                else if (parameters.Count == 0)
                {
                    return constructor.Invoke(null);
                }
            }

            return null;
        }
        #endregion
    }
}
