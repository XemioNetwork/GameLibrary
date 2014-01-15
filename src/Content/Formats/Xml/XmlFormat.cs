using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Formats.Xml
{
    public class XmlFormat : IFormat
    {
        #region Implementation of IFormat
        /// <summary>
        /// Gets a value indicating whether the format supports tags.
        /// </summary>
        public bool SupportsTags
        {
            get { return true; }
        }
        /// <summary>
        /// Creates a writer.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public IFormatWriter CreateWriter(Stream stream)
        {
            return new XmlWriter(stream);
        }
        /// <summary>
        /// Creates a reader.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public IFormatReader CreateReader(Stream stream)
        {
            return new XmlReader(stream);
        }
        #endregion
    }
}
