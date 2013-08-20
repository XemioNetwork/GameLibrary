using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.Formats.Binary
{
    public class BinaryFormat : IFormat
    {
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Id
        {
            get { return ".dat"; }
        }
        #endregion

        #region Implementation of IFormat
        /// <summary>
        /// Creates a writer.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public IFormatWriter CreateWriter(Stream stream)
        {
            return new BinaryWriter(stream);
        }
        /// <summary>
        /// Creates a reader.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public IFormatReader CreateReader(Stream stream)
        {
            return new BinaryReader(stream);
        }
        #endregion
    }
}
