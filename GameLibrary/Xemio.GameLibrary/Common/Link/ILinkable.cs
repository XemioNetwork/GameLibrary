using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Link
{
    public interface ILinkable<T>
    {
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        T Id { get; }
    }
    public interface ILinkable : ILinkable<int>
    {
    }
}
