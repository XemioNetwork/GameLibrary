using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization.Automatic
{
    internal class DictionaryProcessor : IAutomaticProcessor
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
            if (genericArguments.Length != 2)
                return false;

            var dictType = typeof(IDictionary<,>).MakeGenericType(genericArguments);
            return dictType.IsAssignableFrom(type);
        }
        /// <summary>
        /// Reads an instance of the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        public object Read(IFormatReader reader, Type type)
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
                        TypeHelper.ReadTypedInstance(keyType, reader),
                        TypeHelper.ReadTypedInstance(valueType, reader));
                }
            }

            return dictionary;
        }
        /// <summary>
        /// Writes the specified instance.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="instance">The instance.</param>
        public void Write(IFormatWriter writer, object instance)
        {
            var dictionary = (IDictionary)instance;

            Type keyType = ReflectionCache.GetGenericArguments(dictionary.GetType()).First();
            Type valueType = ReflectionCache.GetGenericArguments(dictionary.GetType()).Last();

            writer.WriteInteger("Length", dictionary.Count);
            foreach (DictionaryEntry entry in dictionary)
            {
                using (writer.Section("Entry"))
                {
                    TypeHelper.WriteTypedInstance(entry.Key, keyType, writer);
                    TypeHelper.WriteTypedInstance(entry.Value, valueType, writer);
                }
            }
        }
        #endregion
    }
}
