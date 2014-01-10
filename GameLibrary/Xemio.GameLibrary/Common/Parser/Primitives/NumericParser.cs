using System;

namespace Xemio.GameLibrary.Common.Parser.Primitives
{
    public class NumericParser : ITypedParser
    {
        #region Implementation of ITypeConverter
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
