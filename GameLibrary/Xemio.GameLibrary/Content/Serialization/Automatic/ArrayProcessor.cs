using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization.Automatic
{
    internal class ArrayProcessor : IAutomaticProcessor
    {
        #region Static Fields
        private static readonly MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray", BindingFlags.Public | BindingFlags.Static);
        #endregion

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
            return type.IsArray;
        }
        /// <summary>
        /// Reads an instance of the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        public object Read(IFormatReader reader, Type type)
        {
            Type elementType = ReflectionCache.GetElementType(type);

            var processor = new ListProcessor();
            var list = (IList)processor.Read(reader, typeof(IList<>).MakeGenericType(elementType));

            return (Array)toArrayMethod
                .MakeGenericMethod(elementType)
                .Invoke(null, new object[] { list });
        }
        /// <summary>
        /// Writes the specified instance.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="instance">The instance.</param>
        public void Write(IFormatWriter writer, object instance)
        {
            var array = (Array)instance;

            writer.WriteInteger("Length", array.Length);
            foreach (object item in array)
            {
                using (writer.Section("Entry"))
                {
                    TypeHelper.WriteTypedInstance(item, ReflectionCache.GetElementType(array.GetType()), writer);
                }
            }
        }
        #endregion
    }
}
