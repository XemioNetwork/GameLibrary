using System;
using System.Globalization;
using System.Linq;
using Xemio.GameLibrary.Common.Conversion;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Common.Conversion.Primitives
{
    public class RectangleConverter : ITypeConverter
    {
        #region Implementation of ITypeConverter
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ConversionResult Convert(string input)
        {
            double temporaryValue;
            string[] dimensions = input.Split(',');

            if (dimensions.Length == 4 &&
                dimensions.All(d => double.TryParse(d,
                    NumberStyles.Number,
                    CultureInfo.CreateSpecificCulture("en-US"),
                    out temporaryValue)))
            {
                var value = new Rectangle(
                    (float)double.Parse(dimensions[0]),
                    (float)double.Parse(dimensions[1]),
                    (float)double.Parse(dimensions[2]),
                    (float)double.Parse(dimensions[3]));

                return new ConversionResult(true, value);
            }

            return new ConversionResult(false, Rectangle.Empty);
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
