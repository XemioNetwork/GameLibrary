using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.CSS.Parsers
{
    public class StyleParser : IParser<Style>
    {
        #region Constructors
        public StyleParser()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Implementation of IParser<Style>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public Style Parse(string input)
        {
            WhiteSpaceParser whiteSpaceParser = new WhiteSpaceParser();
            Style style = new Style();

            string code = whiteSpaceParser.Parse(input);

        }
        #endregion
    }
}
