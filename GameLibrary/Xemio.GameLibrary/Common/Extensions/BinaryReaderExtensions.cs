using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Common.Extensions
{
    public static class BinaryReaderExtensions
    {
        #region Private Methods
        /// <summary>
        /// Reads an array.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private static Array ReadArray(BinaryReader reader, Type type)
        {
            int length = reader.ReadInt32();
            object[] array = new object[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = reader.ReadInstance(type.GetElementType());
            }

            return array;
        }
        /// <summary>
        /// Determines whether the specified type is a generic dictionary.
        /// </summary>
        /// <param name="type">The type.</param>
        private static bool IsGenericDictionary(Type type)
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
        private static bool IsGenericList(Type type)
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
        private static IList ReadList(BinaryReader reader, Type type)
        {
            Type elementType = type.GetGenericArguments().First();
            Type listType = typeof(List<>).MakeGenericType(elementType);

            int count = reader.ReadInt32();
            IList list = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i < count; i++)
            {
                list.Add(reader.ReadInstance(elementType));
            }

            return list;
        }
        /// <summary>
        /// Reads a dictionary.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private static IDictionary ReadDictionary(BinaryReader reader, Type type)
        {
            Type[] genericArguments = type.GetGenericArguments();

            Type keyType = genericArguments[0];
            Type valueType = genericArguments[1];

            Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);

            int count = reader.ReadInt32();
            IDictionary dictionary = (IDictionary)Activator.CreateInstance(dictionaryType);

            for (int i = 0; i < count; i++)
            {
                dictionary.Add(
                    reader.ReadInstance(keyType),
                    reader.ReadInstance(valueType));
            }

            return dictionary;
        }
        /// <summary>
        /// Reads an enum.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private static object ReadEnum(BinaryReader reader, Type type)
        {
            return Enum.ToObject(type, reader.ReadInt32());
        }
        /// <summary>
        /// Reads all properties for the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private static IEnumerable<KeyValuePair<PropertyInfo, object>> ReadProperties(BinaryReader reader, Type type)
        {
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (!property.GetCustomAttributes(true).Any(attribute => attribute is ExcludeSyncAttribute))
                {
                    bool isNull = reader.ReadBoolean();
                    var propertyValue = isNull ? null : reader.ReadInstance(property.PropertyType);

                    yield return new KeyValuePair<PropertyInfo, object>(property, propertyValue);
                }
            }
        }

        /// <summary>
        /// Reads a all properties for the specified type and creates an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private static object ReadKnownObject(BinaryReader reader, Type type)
        {
            object instance = null;
            PropertyInfo[] properties = type.GetProperties();
            var propertyValues = ReadProperties(reader, type).ToList();

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
        /// <summary>
        /// Reads a reference based object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        private static object ReadReferenceBasedObject(BinaryReader reader, Type type)
        {
            if (type.IsArray)
                return ReadArray(reader, type);

            if (IsGenericDictionary(type))
                return ReadDictionary(reader, type);

            if (IsGenericList(type)) 
                return ReadList(reader, type);

            if (type.IsEnum)
                return ReadEnum(reader, type);

            if (type == typeof(Vector2)) 
                return reader.ReadVector2();

            if (type == typeof(Rectangle))
                return reader.ReadRectangle();

            //If the specified type isn't any known special case,
            //just read all properties and create an instance.

            return ReadKnownObject(reader, type);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads a vector2.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();

            return new Vector2(x, y);
        }
        /// <summary>
        /// Reads a rectangle.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public static Rectangle ReadRectangle(this BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            float width = reader.ReadSingle();
            float height = reader.ReadSingle();

            return new Rectangle(x, y, width, height);
        }
        /// <summary>
        /// Reads an object of the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object ReadInstance(this BinaryReader reader, Type type)
        {
            object value = null;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    value = reader.ReadBoolean();
                    break;
                case TypeCode.Byte:
                    value = reader.ReadByte();
                    break;
                case TypeCode.Char:
                    value = reader.ReadChar();
                    break;
                case TypeCode.Decimal:
                    value = reader.ReadDecimal();
                    break;
                case TypeCode.Double:
                    value = reader.ReadDouble();
                    break;
                case TypeCode.Int16:
                    value = reader.ReadInt16();
                    break;
                case TypeCode.Int32:
                    value = reader.ReadInt32();
                    break;
                case TypeCode.Int64:
                    value = reader.ReadInt64();
                    break;
                case TypeCode.SByte:
                    value = reader.ReadSByte();
                    break;
                case TypeCode.Single:
                    value = reader.ReadSingle();
                    break;
                case TypeCode.String:
                    value = reader.ReadString();
                    break;
                case TypeCode.UInt16:
                    value = reader.ReadUInt16();
                    break;
                case TypeCode.UInt32:
                    value = reader.ReadUInt32();
                    break;
                case TypeCode.UInt64:
                    value = reader.ReadUInt64();
                    break;
                default:
                    value = ReadReferenceBasedObject(reader, type);
                    break;
            }

            return value;
        }
        #endregion
    }
}
