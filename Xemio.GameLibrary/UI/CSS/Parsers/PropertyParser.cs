using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.CSS.Properties;

namespace Xemio.GameLibrary.UI.CSS.Parsers
{
    public class PropertyParser : IParser<IStyleProperty>
    {
        #region Implementation of IParser<IStyleProperty>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public IStyleProperty Parse(string input)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
