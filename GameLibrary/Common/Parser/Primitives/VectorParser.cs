﻿using System;
using System.Globalization;
using System.Linq;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Common.Parser.Primitives
{
    public class VectorParser : ITypedParser
    {
        #region Implementation of ITypeConverter
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ParserResult Parse(string input)
        {
            double temporaryValue;
            string[] dimensions = input.Split(',');

            if (dimensions.Length == 2 &&
                dimensions.All(d => double.TryParse(d,
                    NumberStyles.Number,
                    CultureInfo.CreateSpecificCulture("en-US"),
                    out temporaryValue)))
            {
                var value = new Vector2(
                    (float)double.Parse(dimensions[0]),
                    (float)double.Parse(dimensions[1]));

                return new ParserResult(true, value);
            }

            return new ParserResult(false, Vector2.Zero);
        }
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return typeof(Vector2); }
        }
        #endregion
    }
}
