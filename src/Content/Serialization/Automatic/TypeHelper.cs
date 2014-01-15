using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization.Automatic
{
    public static class TypeHelper
    {
        #region Methods
        /// <summary>
        /// Reads a typed instance from the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public static T ReadTypedInstance<T>(IFormatReader reader)
        {
            return (T)TypeHelper.ReadTypedInstance(typeof (T), reader);
        }
        /// <summary>
        /// Reads a typed instance from the specified reader.
        /// </summary>
        /// <param name="defaultType">The default type.</param>
        /// <param name="reader">The reader.</param>
        public static object ReadTypedInstance(Type defaultType, IFormatReader reader)
        {
            var serializer = XGL.Components.Require<SerializationManager>();
            return serializer.Load(TypeHelper.ReadType(defaultType, reader), reader);
        }
        /// <summary>
        /// Writes the specified instance as typed object.
        /// </summary>
        /// <typeparam name="TDefault">The type of the default.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="writer">The writer.</param>
        public static void WriteTypedInstance<TDefault>(object instance, IFormatWriter writer)
        {
            TypeHelper.WriteTypedInstance(instance, typeof(TDefault), writer);
        }
        /// <summary>
        /// Writes the specified instance as typed object.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="defaultType">The default type.</param>
        /// <param name="writer">The writer.</param>
        public static void WriteTypedInstance(object instance, Type defaultType, IFormatWriter writer)
        {
            var serializer = XGL.Components.Require<SerializationManager>();

            TypeHelper.WriteType(instance.GetType(), defaultType, writer);
            serializer.Save(instance, writer);
        }
        /// <summary>
        /// Reads the type.
        /// </summary>
        /// <param name="defaultType">The default type.</param>
        /// <param name="reader">The reader.</param>
        public static Type ReadType(Type defaultType, IFormatReader reader)
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
        /// <param name="type">The type.</param>
        /// <param name="defaultType">The default type.</param>
        /// <param name="writer">The writer.</param>
        public static void WriteType(Type type, Type defaultType, IFormatWriter writer)
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
        #endregion
    }
}
