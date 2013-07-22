using System;
using System.Globalization;
using System.Linq;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Common.TypeParsers.BaseTypes
{
    public class RectangleParser : ITypeParser
    {
        #region Implementation of ITypeParser
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ParserResult Parse(string input)
        {
            double temporaryValue;
            string[] dimensions = input.Split(';');

            if (dimensions.Length == 4 &&
                dimensions.All(d => double.TryParse(d,
                    NumberStyles.Number,
                    CultureInfo.CreateSpecificCulture("en-US"),
                    out temporaryValue)))
            {
                Rectangle value = new Rectangle(
                    (float)double.Parse(dimensions[0]),
                    (float)double.Parse(dimensions[1]),
                    (float)double.Parse(dimensions[2]),
                    (float)double.Parse(dimensions[3]));

                return new ParserResult(true, value);
            }

            return new ParserResult(false, Rectangle.Empty);
        }
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return typeof(Rectangle); }
        }
        #endregion
    }
}
