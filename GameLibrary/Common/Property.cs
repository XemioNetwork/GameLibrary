using System;

namespace Xemio.GameLibrary.Common
{
    internal class Property
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="lineNumber">The line number.</param>
        public Property(String key, string value, int lineNumber)
        {
            this.Key = key;
            this.Value = value;
            this.LineNumber = lineNumber;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Gets the line number.
        /// </summary>
        public int LineNumber { get; private set; }
        #endregion
    }
}
