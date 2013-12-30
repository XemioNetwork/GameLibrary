using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization.Automatic
{
    internal class ListProcessor : IAutomaticProcessor
    {
        #region Implementation of IAutomaticProcessor
        /// <summary>
        /// Gets the priority.
        /// </summary>
        public int Priority
        {
            get { return 100; }
        }
        /// <summary>
        /// Determines whether this instance can process the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public bool CanProcess(Type type)
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
        /// Reads an instance of the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        public object Read(IFormatReader reader, Type type)
        {
            Type elementType = ReflectionCache.GetGenericArguments(type).First();
            Type listType = typeof(List<>).MakeGenericType(elementType);

            int count = reader.ReadInteger("Length");
            var list = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i < count; i++)
            {
                using (reader.Section("Entry"))
                {
                    list.Add(TypeHelper.ReadTypedInstance(elementType, reader));
                }
            }

            return list;
        }
        /// <summary>
        /// Writes the specified instance.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="instance">The instance.</param>
        public void Write(IFormatWriter writer, object instance)
        {
            var list = (IList)instance;
            Type elementType = ReflectionCache.GetGenericArguments(list.GetType()).Single();

            writer.WriteInteger("Length", list.Count);
            foreach (object item in list)
            {
                using (writer.Section("Entry"))
                {
                    TypeHelper.WriteTypedInstance(item, elementType, writer);
                }
            }
        }
        #endregion
    }
}
