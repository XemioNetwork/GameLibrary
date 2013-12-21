using System;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Common.Conversion
{
    public interface ITypeConverter : IConverter<string, ConversionResult>, ILinkable<Type>
    {
    }
}
