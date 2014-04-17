using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Metadata
{
    public interface IMetadata
    {
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        Guid Guid { get; }
        /// <summary>
        /// Gets the type.
        /// </summary>
        Type Type { get; }
    }
}
