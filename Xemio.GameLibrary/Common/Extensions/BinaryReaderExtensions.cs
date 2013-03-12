using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Xemio.GameLibrary.Common.Extensions
{
    public static class BinaryReaderExtensions
    {
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
                    if (type.IsArray)
                    {
                        int length = reader.ReadInt32();
                        object[] array = new object[length];

                        for (int i = 0; i < length; i++)
                        {
                            array[i] = reader.ReadInstance(type.GetElementType());
                        }

                        value = array;
                    }
                    else if (type.IsEnum)
                    {
                        value = Enum.ToObject(type, reader.ReadInt32());
                    }
                    else
                    {
                        value = Activator.CreateInstance(type);

                        PropertyInfo[] properties = type.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            if (!property.GetCustomAttributes(true)
                                .Any(attribute => attribute is ExcludeSyncAttribute))
                            {
                                bool isNull = reader.ReadBoolean();
                                object propertyValue = isNull ? null : reader.ReadInstance(property.PropertyType);
                                
                                property.SetValue(value, propertyValue, null);
                            }
                        }
                    }
                    break;
            }

            return value;
        }
    }
}
