using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Formats.None
{
    public class FormatlessFormat : IFormat
    {
        #region Implementation of IFormat
        /// <summary>
        /// Gets a value indicating whether the format supports tags.
        /// </summary>
        public bool SupportsTags
        {
            get { return false; }
        }
        /// <summary>
        /// Creates a writer.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public IFormatWriter CreateWriter(Stream stream)
        {
            return new FormatlessWriter(stream);
        }
        /// <summary>
        /// Creates a reader.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public IFormatReader CreateReader(Stream stream)
        {
            return new FormatlessReader(stream);
        }
        #endregion
    }
}
