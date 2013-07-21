using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.TypeParsers
{
    public class DoubleParser : ITypeParser
    {
        #region Implementation of ITypeParser
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public object Parse(string input)
        {
            return double.Parse(input, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"));
        }
        /// <summary>
        /// Determines whether the specified input is from the specified type.
        /// </summary>
        /// <param name="input">The input.</param>
        public bool IsType(string input)
        {
            double value;

            return double.TryParse(input,
                NumberStyles.Number,
                CultureInfo.CreateSpecificCulture("en-US"),
                out value);
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
