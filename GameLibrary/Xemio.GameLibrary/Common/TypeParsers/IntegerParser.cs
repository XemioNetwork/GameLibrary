using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Common.TypeParsers
{
    public class IntegerParser : ITypeParser
    {
        #region Implementation of ITypeParser
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public object Parse(string input)
        {
            return int.Parse(input);
        }
        /// <summary>
        /// Determines whether the specified input is from the specified type.
        /// </summary>
        /// <param name="input">The input.</param>
        public bool IsType(string input)
        {
            int value;
            return int.TryParse(input, out value);
        }
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return typeof(int); }
        }
        #endregion
    }
}
