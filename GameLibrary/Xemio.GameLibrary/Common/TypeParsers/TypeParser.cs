using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Common.TypeParsers
{
    public class TypeParser
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeParser"/> class.
        /// </summary>
        public TypeParser()
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
                if (typeParser.IsType(value))
                {
                    return typeParser.Parse(value);
                }
            }

            return value;
        }
        #endregion
    }
}
