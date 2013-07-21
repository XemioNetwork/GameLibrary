using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Common.TypeParsers
{
    public interface ITypeParser : IParser<string, object>, ILinkable<Type>
    {
        /// <summary>
        /// Determines whether the specified input is from the specified type.
        /// </summary>
        /// <param name="input">The input.</param>
        bool IsType(string input);
    }
}
