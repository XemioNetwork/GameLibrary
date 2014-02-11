using System;

namespace Xemio.GameLibrary.Common.Parser.Primitives
{
    public class BooleanParser : ITypedParser
    {
        #region Implementation of ITypeConverter
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ParserResult Parse(string input)
        {
            bool value;
            bool succeed = bool.TryParse(input, out value);

            return new ParserResult(succeed, value);
        }
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return typeof(bool); }
        }
        #endregion
    }
}
