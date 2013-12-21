using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.Formats.Binary;

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
        #endregion
    }
}
