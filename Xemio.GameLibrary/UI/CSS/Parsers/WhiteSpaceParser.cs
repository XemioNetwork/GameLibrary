using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.CSS.Parsers
{
    public class WhiteSpaceParser : IParser<string>
    {
        #region Implementation of IParser<string>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public string Parse(string input)
        {
            string code = input.Replace("\n", string.Empty);

            code = code.Replace("\r", string.Empty);
            code = code.Replace("\t", " ");

            while (code.Contains("  "))
            {
                code = code.Replace("  ", " ");
            }

            return code;
        }
        #endregion
    }
}
