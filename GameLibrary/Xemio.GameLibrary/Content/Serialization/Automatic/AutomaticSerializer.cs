using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content.Attributes;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Serialization.Automatic
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
            this.Type = type;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
        #endregion

        #region Static Properties
        /// <summary>
        /// The list containing all processors.
        /// </summary>
        private static readonly List<IAutomaticProcessor> processors = new List<IAutomaticProcessor>()
        {
            new ArrayProcessor(),
            new DictionaryProcessor(),
            new EnumProcessor(),
            new ListProcessor(),
            new PropertyProcessor()
        };
        #endregion
        
        #region Static Methods
        /// <summary>
        /// Gets the processor.
        /// </summary>
        /// <param name="type">The type.</param>
        public static IAutomaticProcessor GetProcessor(Type type)
        {
            IAutomaticProcessor processor = processors
                .OrderByDescending(p => p.Priority)
                .FirstOrDefault(p => p.CanProcess(type));

            if (processor == null)
            {
                throw new InvalidOperationException("No automatic processor was found for type " + type);
            }

            return processor;
        }
        #endregion

        #region Overrides of Serializer<object>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override object Read(IFormatReader reader)
        {
            return AutomaticSerializer.GetProcessor(this.Type).Read(reader, this.Type);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, object value)
        {
            AutomaticSerializer.GetProcessor(this.Type).Write(writer, value);
        }
        #endregion
    }
}
