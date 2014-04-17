using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common.References
{
    public interface IReference<out T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        T Value { get; }
    }
}
