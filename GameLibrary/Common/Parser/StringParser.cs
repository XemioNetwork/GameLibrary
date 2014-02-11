using System;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Common.Parser
{
    public class StringParser
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringParser"/> class.
        /// </summary>
        public StringParser()
        {
            this._implementations = XGL.Components.Get<IImplementationManager>();
        }
        #endregion

        #region Fields
        private readonly IImplementationManager _implementations;
        #endregion

        #region Methods
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object Parse(string value)
        {
            foreach (ITypedParser parser in this._implementations.All<Type, ITypedParser>())
            {
                ParserResult result = parser.Parse(value);
                if (result.Succeed)
                {
                    return result.Value;
                }
            }

            return value;
        }
        #endregion
    }
}
