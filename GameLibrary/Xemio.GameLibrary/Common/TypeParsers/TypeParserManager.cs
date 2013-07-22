using System;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Common.TypeParsers
{
    public class TypeParserManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeParserManager"/> class.
        /// </summary>
        public TypeParserManager()
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
            foreach (ITypeParser typeParser in this._implementations.All<Type, ITypeParser>())
            {
                ParserResult result = typeParser.Parse(value);
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
