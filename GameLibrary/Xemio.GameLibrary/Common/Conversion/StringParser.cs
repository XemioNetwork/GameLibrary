using System;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Common.Conversion
{
    public class StringParser
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringParser"/> class.
        /// </summary>
        public StringParser()
        {
            this._implementations = XGL.Components.Get<ImplementationManager>();
        }
        #endregion

        #region Fields
        private readonly ImplementationManager _implementations;
        #endregion

        #region Methods
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object Parse(string value)
        {
            foreach (ITypeConverter converter in this._implementations.All<Type, ITypeConverter>())
            {
                ConversionResult result = converter.Convert(value);
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
