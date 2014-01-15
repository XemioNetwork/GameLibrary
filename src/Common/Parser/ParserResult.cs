namespace Xemio.GameLibrary.Common.Parser
{
    public struct ParserResult
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserResult"/> struct.
        /// </summary>
        /// <param name="succeed">if set to <c>true</c> [succeed].</param>
        /// <param name="value">The value.</param>
        public ParserResult(bool succeed, object value)
        {
            this._succeed = succeed;
            this._value = value;
        }
        #endregion

        #region Fields
        private readonly bool _succeed;
        private readonly object _value;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ParserResult"/> is succeed.
        /// </summary>
        public bool Succeed
        {
            get { return this._succeed; }
        }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value
        {
            get { return this._value; }
        }
        #endregion
    }
}
