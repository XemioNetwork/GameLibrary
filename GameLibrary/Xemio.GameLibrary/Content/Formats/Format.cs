using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.Formats.Binary;
using Xemio.GameLibrary.Content.Formats.Xml;

namespace Xemio.GameLibrary.Content.Formats
{
    public class Format
    {
        #region Formats
        /// <summary>
        /// Gets the binary format.
        /// </summary>
        public static IFormat Binary
        {
            get { return new BinaryFormat(); }
        }
        /// <summary>
        /// Gets the XML format.
        /// </summary>
        public static IFormat Xml
        {
            get{ return new XmlFormat(); }
        }
        #endregion
    }
}
