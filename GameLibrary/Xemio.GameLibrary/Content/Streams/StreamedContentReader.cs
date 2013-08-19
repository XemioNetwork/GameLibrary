using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Streams
{
    public abstract class StreamedContentReader<T> : ContentReader<T>
    {
        #region Methods
        /// <summary>
        /// Reads a value out of the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        protected abstract T Read(Stream stream);
        #endregion

        #region Overrides of ContentReader<T>
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override T Read(IFormatReader reader)
        {
            return this.Read(reader.Stream);
        }
        #endregion
    }
}
