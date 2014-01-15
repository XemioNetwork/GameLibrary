using System;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization.Automatic
{
    internal class EnumProcessor : IAutomaticProcessor
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
            return type.IsEnum;
        }
        /// <summary>
        /// Reads an instance of the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        public object Read(IFormatReader reader, Type type)
        {
            return Enum.ToObject(type, reader.ReadInteger("Value"));
        }
        /// <summary>
        /// Writes the specified instance.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="instance">The instance.</param>
        public void Write(IFormatWriter writer, object instance)
        {
            writer.WriteInteger("Value", (int)instance);
        }
        #endregion
    }
}
