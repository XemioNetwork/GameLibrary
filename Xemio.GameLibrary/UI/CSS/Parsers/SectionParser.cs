using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.CSS.Parsers
{
    public class SectionParser : IParser<StyleSection>
    {
        #region Implementation of IParser<StyleSection>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public StyleSection Parse(string input)
        {
            PropertyParser propertyParser = new PropertyParser();

        }
        #endregion
    }
}
