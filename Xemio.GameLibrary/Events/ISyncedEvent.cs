using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events
{
    public interface ISyncedEvent : IEvent
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ISyncedEvent"/> is synced.
        /// </summary>
        bool Synced { get; }
    }
}
