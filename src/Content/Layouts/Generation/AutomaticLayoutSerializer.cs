using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Generation
{
    [ManuallyLinked]
    public class AutomaticLayoutSerializer : Serializer<object>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticLayoutSerializer"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AutomaticLayoutSerializer(Type type)
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

        #region Overrides of Serializer<object>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override object Read(IFormatReader reader)
        {
            object instance = Activator.CreateInstance(this.Type, true);
            LayoutGenerator.Cache.Get(this.Type).Read(reader, instance);

            return instance;
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, object value)
        {
            LayoutGenerator.Cache.Get(this.Type).Write(writer, value);
        }
        #endregion
    }
}
