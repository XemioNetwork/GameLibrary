using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content.Attributes;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Serialization
{
    [ManuallyLinked]
    public class AutomaticSerializer : Serializer<object>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticSerializer"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AutomaticSerializer(Type type)
        {
            this._serializer = XGL.Components.Require<SerializationManager>();
            this.Type = type;
        }
        #endregion

        #region Fields
        private readonly SerializationManager _serializer;
        private static readonly MethodInfo _toArrayMethod = typeof (Enumerable).GetMethod("ToArray", BindingFlags.Public | BindingFlags.Static);
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
        #endregion

        #region Read Methods
        /// <summary>
        /// Reads an array.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private Array ReadArray(IFormatReader reader, Type type)
        {
            Type elementType = ReflectionCache.GetElementType(type);
            IList list = this.ReadList(reader, typeof(IList<>).MakeGenericType(elementType));
            
            return (Array)_toArrayMethod
                .MakeGenericMethod(elementType)
                .Invoke(null, new object[] { list });
        }
        /// <summary>
        /// Reads a type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="defaultType">The default type.</param>
        private Type ReadType(IFormatReader reader, Type defaultType)
        {
            if (defaultType != typeof(string))
            {
                bool isInherited = reader.ReadBoolean("IsInherited");

                if (isInherited)
                {
                    string typeName = reader.ReadString("Type");
                    return Type.GetType(typeName);
                }
            }

            return defaultType;
        }
        /// <summary>
        /// Writes the type.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="type">The type.</param>
        /// <param name="defaultType">The default type.</param>
        private void WriteType(IFormatWriter writer, Type type, Type defaultType)
        {
            if (type != typeof(string))
            {
                bool isInherited = (defaultType != type);
                writer.WriteBoolean("IsInherited", isInherited);

                if (isInherited)
                {
                    writer.WriteString("Type", type.AssemblyQualifiedName);
                }
            }
        }
        /// <summary>
        /// Determines whether the specified type is a generic dictionary.
        /// </summary>
        /// <param name="type">The type.</param>
        private bool IsGenericDictionary(Type type)
        {
            if (!type.IsGenericType)
                return false;

            var genericArguments = ReflectionCache.GetGenericArguments(type);
            if (genericArguments.Length != 2)
                return false;

            var dictType = typeof(IDictionary<,>).MakeGenericType(genericArguments);
            return dictType.IsAssignableFrom(type);
        }
        /// <summary>
        /// Determines whether the specified type is a generic list.
        /// </summary>
        /// <param name="type">The type.</param>
        private bool IsGenericList(Type type)
        {
            if (!type.IsGenericType)
                return false;

            var genericArguments = ReflectionCache.GetGenericArguments(type);
            if (genericArguments.Length != 1)
                return false;

            var listType = typeof(IList<>).MakeGenericType(genericArguments);
            return listType.IsAssignableFrom(type);
        }
        /// <summary>
        /// Reads a list.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private IList ReadList(IFormatReader reader, Type type)
        {
            Type elementType = ReflectionCache.GetGenericArguments(type).First();
            Type listType = typeof(List<>).MakeGenericType(elementType);

            int count = reader.ReadInteger("Length");
            var list = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i < count; i++)
            {
                using (reader.Section("Entry"))
                {
                    list.Add(this._serializer.Load(this.ReadType(reader, elementType), reader));
                }
            }

            return list;
        }
        /// <summary>
        /// Reads a dictionary.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private IDictionary ReadDictionary(IFormatReader reader, Type type)
        {
            Type[] genericArguments = ReflectionCache.GetGenericArguments(type);

            Type keyType = genericArguments[0];
            Type valueType = genericArguments[1];

            Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);

            int count = reader.ReadInteger("Length");
            var dictionary = (IDictionary)Activator.CreateInstance(dictionaryType);

            for (int i = 0; i < count; i++)
            {
                using (reader.Section("Entry"))
                {
                    dictionary.Add(
                        this._serializer.Load(this.ReadType(reader, keyType), reader),
                        this._serializer.Load(this.ReadType(reader, valueType), reader));
                }
            }

            return dictionary;
        }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private object SelectReadMethod(IFormatReader reader, Type type)
        {
            object instance;

            if (type.IsArray)
            {
                instance = this.ReadArray(reader, type);
            }
            else if (this.IsGenericDictionary(type))
            {
                instance = this.ReadDictionary(reader, type);
            }
            else if (this.IsGenericList(type))
            {
                instance = this.ReadList(reader, type);
            }
            else if (type.IsEnum)
            {
                instance = Enum.ToObject(type, reader.ReadInteger("Value"));
            }
            else
            {
                instance = this.ReadProperties(reader, type);
            }

            return instance;
        }
        /// <summary>
        /// Creates the instance with constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="properties">The properties.</param>
        private object CreateInstanceWithConstructor(Type type, IEnumerable<KeyValuePair<PropertyInfo, object>> properties)
        {
            PropertyInfo[] propertyInfos = ReflectionCache.GetProperties(type);

            foreach (ConstructorInfo constructor in ReflectionCache.GetConstructors(type))
            {
                List<ParameterInfo> parameters = ReflectionCache.GetConstructorParameters(constructor).ToList();

                bool validConstructor =
                    parameters.All(
                        parameter => propertyInfos.Any(
                            property => property.Name.ToLower() == parameter.Name.ToLower()));

                if (validConstructor)
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
        /// <summary>
        /// Reads a all properties for the specified type and creates an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private object ReadProperties(IFormatReader reader, Type type)
        {
            var propertyValues = new Dictionary<PropertyInfo, object>();

            foreach (PropertyInfo property in ReflectionCache.GetProperties(type))
            {
                if (!ReflectionCache.HasCustomAttribute<ExcludeSerializationAttribute>(property))
                {
                    using (reader.Section(property.Name))
                    {
                        bool isValueType = property.PropertyType.IsValueType;
                        bool isNull = (isValueType == false && reader.ReadBoolean("IsNull"));

                        Type propertyType = !isValueType && !isNull
                            ? this.ReadType(reader, property.PropertyType)
                            : property.PropertyType;

                        propertyValues.Add(property, isNull ? null : this._serializer.Load(propertyType, reader));
                    }
                }
            }

            object instance = this.CreateInstanceWithConstructor(type, propertyValues);

            foreach (KeyValuePair<PropertyInfo, object> pair in propertyValues)
            {
                if (ReflectionCache.GetSetMethod(pair.Key) != null)
                {
                    pair.Key.SetValue(instance, pair.Value, null);
                }
            }

            return instance;
        }
        #endregion

        #region Write Methods
        /// <summary>
        /// Writes the specified array.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteArray(IFormatWriter writer, object value)
        {
            var array = (Array)value;

            writer.WriteInteger("Length", array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                using (writer.Section("Entry"))
                {
                    this.WriteType(writer, array.GetValue(i).GetType(), ReflectionCache.GetElementType(array.GetType()));
                    this._serializer.Save(array.GetValue(i), writer);
                }
            }
        }
        /// <summary>
        /// Writes the specified list.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteList(IFormatWriter writer, object value)
        {
            var list = (IList)value;
            Type elementType = ReflectionCache.GetGenericArguments(list.GetType()).Single();

            writer.WriteInteger("Length", list.Count);
            foreach (object instance in list)
            {
                using (writer.Section("Entry"))
                {
                    this.WriteType(writer, instance.GetType(), elementType);
                    this._serializer.Save(instance, writer);
                }
            }
        }
        /// <summary>
        /// Writes the specified dictionary.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteDictionary(IFormatWriter writer, object value)
        {
            var dictionary = (IDictionary)value;

            Type keyType = ReflectionCache.GetGenericArguments(dictionary.GetType()).First();
            Type valueType = ReflectionCache.GetGenericArguments(dictionary.GetType()).Last();

            writer.WriteInteger("Length", dictionary.Count);
            foreach (DictionaryEntry entry in dictionary)
            {
                using (writer.Section("Entry"))
                {
                    this.WriteType(writer, entry.Key.GetType(), keyType);
                    this._serializer.Save(entry.Key, writer);

                    this.WriteType(writer, entry.Value.GetType(), valueType);
                    this._serializer.Save(entry.Value, writer);
                }
            }
        }
        /// <summary>
        /// Writes all object properties.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteProperties(IFormatWriter writer, object value)
        {
            PropertyInfo[] properties = ReflectionCache.GetProperties(value.GetType());
            foreach (PropertyInfo property in properties)
            {
                if (!ReflectionCache.HasCustomAttribute<ExcludeSerializationAttribute>(property))
                {
                    using (writer.Section(property.Name))
                    {
                        object propertyValue = property.GetValue(value, null);
                        bool isNull = (propertyValue == null);

                        if (property.PropertyType.IsValueType == false)
                        {
                            writer.WriteBoolean("IsNull", isNull);
                            if (isNull == false)
                            {
                                this.WriteType(writer, propertyValue.GetType(), property.PropertyType);
                            }
                        }

                        if (isNull == false)
                        {
                            this._serializer.Save(propertyValue, writer);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Writes the instance.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void SelectWriteMethod(IFormatWriter writer, object value)
        {
            Type type = value.GetType();

            if (type.IsArray)
            {
                this.WriteArray(writer, value);
            }
            else if (typeof(IList).IsAssignableFrom(type))
            {
                this.WriteList(writer, value);
            }
            else if (typeof(IDictionary).IsAssignableFrom(type))
            {
                this.WriteDictionary(writer, value);
            }
            else if (type.IsEnum)
            {
                writer.WriteInteger("Value", (int) value);
            }
            else
            {
                this.WriteProperties(writer, value);
            }
        }
        #endregion
        
        #region Overrides of Serializer<object>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override object Read(IFormatReader reader)
        {
            return this.SelectReadMethod(reader, this.Type);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, object value)
        {
            this.SelectWriteMethod(writer, value);
        }
        #endregion
    }
}
