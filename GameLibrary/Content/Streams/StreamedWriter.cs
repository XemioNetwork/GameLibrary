using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Streams
{
    public abstract class StreamedWriter<T> : Writer<T>
    {
        #region Methods
        /// <summary>
        /// Writes the specified value into the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="value">The value.</param>
        protected abstract void Write(Stream stream, T value);
        #endregion

        #region Overrides of Writer<T>
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The format writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, T value)
        {
            this.Write(writer.Stream, value);
        }
        #endregion
    }
}
