using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Serialization
{
    [ManuallyLinked]
    public class AutomaticSerializer : ContentSerializer<object>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticSerializer"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AutomaticSerializer(Type type)
        {
            this._content = XGL.Components.Require<ContentManager>();
            this.Type = type;
        }
        #endregion

        #region Fields
        private readonly ContentManager _content;
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
            int length = reader.ReadInteger();
            object[] array = new object[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = this._content.Load(type.GetElementType(), reader);
            }

            return array;
        }
        /// <summary>
        /// Determines whether the specified type is a generic dictionary.
        /// </summary>
        /// <param name="type">The type.</param>
        private bool IsGenericDictionary(Type type)
        {
            if (!type.IsGenericType)
                return false;

            var genericArguments = type.GetGenericArguments();
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

            var genericArguments = type.GetGenericArguments();
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
            Type elementType = type.GetGenericArguments().First();
            Type listType = typeof(List<>).MakeGenericType(elementType);

            int count = reader.ReadInteger();
            IList list = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i < count; i++)
            {
                list.Add(this._content.Load(elementType, reader));
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
            Type[] genericArguments = type.GetGenericArguments();

            Type keyType = genericArguments[0];
            Type valueType = genericArguments[1];

            Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);

            int count = reader.ReadInteger();
            IDictionary dictionary = (IDictionary)Activator.CreateInstance(dictionaryType);

            for (int i = 0; i < count; i++)
            {
                dictionary.Add(
                    this._content.Load(keyType, reader),
                    this._content.Load(valueType, reader));
            }

            return dictionary;
        }
        /// <summary>
        /// Reads an enum.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private object ReadEnum(IFormatReader reader, Type type)
        {
            return Enum.ToObject(type, reader.ReadInteger());
        }
        /// <summary>
        /// Reads all properties for the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private IEnumerable<KeyValuePair<PropertyInfo, object>> ReadProperties(IFormatReader reader, Type type)
        {
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.GetCustomAttributes(typeof(ExcludeSerializationAttribute), true).Length == 0)
                {
                    bool isNull = reader.ReadBoolean();
                    var propertyValue = isNull ? null : this._content.Load(property.PropertyType, reader);

                    yield return new KeyValuePair<PropertyInfo, object>(property, propertyValue);
                }
            }
        }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private object ReadInstance(IFormatReader reader, Type type)
        {
            object instance;
            reader.BeginSection();

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
                instance = this.ReadEnum(reader, type);
            }
            else
            {
                instance = this.ReadKnownObject(reader, type);
            }
            
            reader.EndSection();
            return instance;
        }
        /// <summary>
        /// Reads a all properties for the specified type and creates an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private object ReadKnownObject(IFormatReader reader, Type type)
        {
            object instance = null;

            PropertyInfo[] properties = type.GetProperties();
            var propertyValues = this.ReadProperties(reader, type).ToList();

            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                List<ParameterInfo> parameters = constructor.GetParameters().ToList();

                bool validConstructor =
                    parameters.All(
                        parameter => properties.Any(
                            property => property.Name.ToLower() == parameter.Name.ToLower()));

                if (validConstructor)
                {
                    object[] constructorParameters = new object[parameters.Count];

                    foreach (KeyValuePair<PropertyInfo, object> pair in propertyValues)
                    {
                        ParameterInfo parameter = parameters.FirstOrDefault(p => p.Name.ToLower() == pair.Key.Name.ToLower());

                        if (parameter != null)
                        {
                            int index = parameters.IndexOf(parameter);
                            constructorParameters[index] = pair.Value;
                        }
                    }

                    instance = constructor.Invoke(constructorParameters);
                }
                else if (parameters.Count == 0)
                {
                    instance = constructor.Invoke(null);
                }
            }

            foreach (KeyValuePair<PropertyInfo, object> pair in propertyValues)
            {
                if (pair.Key.GetSetMethod() != null)
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
            Array array = (Array)value;

            writer.WriteInteger("Length", array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                this._content.Save(array.GetValue(i), writer);
            }
        }
        /// <summary>
        /// Writes the specified list.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteList(IFormatWriter writer, object value)
        {
            IList list = (IList)value;

            writer.WriteInteger("Count", list.Count);
            foreach (object instance in list)
            {
                this._content.Save(instance, writer);
            }
        }
        /// <summary>
        /// Writes the specified dictionary.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteDictionary(IFormatWriter writer, object value)
        {
            IDictionary dictionary = (IDictionary)value;

            writer.WriteInteger("Count", dictionary.Count);
            foreach (DictionaryEntry entry in dictionary)
            {
                this._content.Save(entry.Key, writer);
                this._content.Save(entry.Value, writer);
            }
        }
        /// <summary>
        /// Writes all object properties.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteProperties(IFormatWriter writer, object value)
        {
            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!property.GetCustomAttributes(true)
                    .Any(attribute => attribute is ExcludeSerializationAttribute))
                {
                    object propertyValue = property.GetValue(value, null);

                    writer.WriteBoolean("IsPropertyNull", propertyValue == null);
                    if (propertyValue != null)
                    {
                        writer.BeginSection(property.Name);
                        this._content.Save(propertyValue, writer);
                        writer.EndSection();
                    }
                }
            }
        }
        /// <summary>
        /// Writes the instance.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        private void WriteInstance(IFormatWriter writer, object value)
        {
            Type type = value.GetType();
            writer.BeginSection(type.Name);

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
                writer.WriteInteger("Enum", (int)value);
            }
            else
            {
                this.WriteProperties(writer, value);
            }

            writer.EndSection();
        }
        #endregion
        
        #region Overrides of ContentSerializer<object>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override object Read(IFormatReader reader)
        {
            return this.ReadInstance(reader, this.Type);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, object value)
        {
            this.WriteInstance(writer, value);
        }
        #endregion
    }
}
