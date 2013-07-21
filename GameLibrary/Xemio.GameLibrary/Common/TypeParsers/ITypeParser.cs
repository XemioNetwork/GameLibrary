using System;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Common.TypeParsers
{
    public interface ITypeParser : IParser<string, ParserResult>, ILinkable<Type>
    {
    }
}
