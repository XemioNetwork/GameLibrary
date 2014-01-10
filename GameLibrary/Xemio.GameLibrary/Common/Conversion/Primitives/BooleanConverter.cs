﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Conversion;

namespace Xemio.GameLibrary.Common.Conversion.Primitives
{
    public class BooleanConverter : ITypeConverter
    {
        #region Implementation of ITypeConverter
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ConversionResult Convert(string input)
        {
            bool value;
            bool succeed = bool.TryParse(input, out value);

            return new ConversionResult(succeed, value);
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