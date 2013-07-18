using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Assets
{
    public interface IAsset
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        object Value { get; }
    }
}
