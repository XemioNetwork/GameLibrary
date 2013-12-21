using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Streams
{
    public abstract class StreamedSerializer<T> : Serializer<T>
    {
        #region Methods
        /// <summary>
        /// Reads a value out of the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        protected abstract T Read(Stream stream);
        /// <summary>
        /// Writes the specified value into the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="value">The value.</param>
        protected abstract void Write(Stream stream, T value);
        #endregion

        #region Overrides of SerializationManager<T>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override T Read(IFormatReader reader)
        {
            return this.Read(reader.Stream);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, T value)
        {
            this.Write(writer.Stream, value);
        }
        #endregion
    }
}
