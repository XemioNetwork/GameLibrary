﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Common.TypeParsers
{
    public class VectorParser : ITypeParser
    {
        #region Implementation of ITypeParser
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public object Parse(string input)
        {
            string[] dimensions = input.Split(',');
            Vector2 value = new Vector2(
                float.Parse(dimensions[0]),
                float.Parse(dimensions[1]));

            return value;
        }
        /// <summary>
        /// Determines whether the specified input is from the specified type.
        /// </summary>
        /// <param name="input">The input.</param>
        public bool IsType(string input)
        {
            string[] dimensions = input.Split(';');
            float value;

            return dimensions.Length == 2 &&
                dimensions.All(d => float.TryParse(d, out value));
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
