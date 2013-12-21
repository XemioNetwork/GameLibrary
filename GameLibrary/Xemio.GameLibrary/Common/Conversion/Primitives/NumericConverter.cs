using System;
using Xemio.GameLibrary.Common.Conversion;

namespace Xemio.GameLibrary.Common.Conversion.Primitives
{
    public class NumericConverter : ITypeConverter
    {
        #region Implementation of ITypeConverter
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ConversionResult Convert(string input)
        {
            double value;
            bool succeed = double.TryParse(input, out value);

            return new ConversionResult(succeed, value);
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
