using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events
{
    public interface IEvent
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IEvent"/> is synced.
        /// </summary>
        bool Synced { get; }
    }
}
