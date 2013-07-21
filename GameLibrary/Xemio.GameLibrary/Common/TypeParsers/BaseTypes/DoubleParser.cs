using System;

namespace Xemio.GameLibrary.Common.TypeParsers.BaseTypes
{
    public class DoubleParser : ITypeParser
    {
        #region Implementation of ITypeParser
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ParserResult Parse(string input)
        {
            double value;
            bool succeed = double.TryParse(input, out value);

            return new ParserResult(succeed, value);
        }
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return typeof(double); }
        }
        #endregion
    }
}
