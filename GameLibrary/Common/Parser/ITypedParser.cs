using System;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Common.Parser
{
    public interface ITypedParser : ILinkable<Type>
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        ParserResult Parse(string input);
    }
}
