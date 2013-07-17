using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Script.Events
{
    public interface IScriptEvent : IEvent
    {
        /// <summary>
        /// Gets the script.
        /// </summary>
        IScript Script { get; }
    }
}
