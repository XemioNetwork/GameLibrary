using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Xemio.GameLibrary.Common.Extensions
{
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes the specified object to the stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public static void WriteProperties(this BinaryWriter writer, object value)
        {
            Type type = value.GetType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    writer.Write((bool)value);
                    break;
                case TypeCode.Byte:
                    writer.Write((byte)value);
                    break;
                case TypeCode.Char:
                    writer.Write((char)value);
                    break;
                case TypeCode.Decimal:
                    writer.Write((decimal)value);
                    break;
                case TypeCode.Double:
                    writer.Write((double)value);
                    break;
                case TypeCode.Int16:
                    writer.Write((short)value);
                    break;
                case TypeCode.Int32:
                    writer.Write((int)value);
                    break;
                case TypeCode.Int64:
                    writer.Write((long)value);
                    break;
                case TypeCode.SByte:
                    writer.Write((sbyte)value);
                    break;
                case TypeCode.Single:
                    writer.Write((float)value);
                    break;
                case TypeCode.String:
                    writer.Write((string)value);
                    break;
                case TypeCode.UInt16:
                    writer.Write((ushort)value);
                    break;
                case TypeCode.UInt32:
                    writer.Write((uint)value);
                    break;
                case TypeCode.UInt64:
                    writer.Write((ulong)value);
                    break;
                default:
                    if (type.IsArray)
                    {
                        Array array = value as Array;
                        
                        writer.Write(array.Length);
                        for (int i = 0; i < array.Length; i++)
                        {
                            writer.WriteProperties(array.GetValue(i));
                        }
                    }
                    else if (type.IsEnum)
                    {
                        writer.Write((int)value);
                    }
                    else
                    {
                        //TODO: Refactor or update...
                        writer.Write(value == null);

                        if (value != null)
                        {
                            PropertyInfo[] properties = type.GetProperties();
                            foreach (PropertyInfo property in properties)
                            {
                                if (!property.GetCustomAttributes(true)
                                    .Any(attribute => attribute is ExcludeAttribute))
                                {
                                    writer.WriteProperties(property.GetValue(value, null));
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}
