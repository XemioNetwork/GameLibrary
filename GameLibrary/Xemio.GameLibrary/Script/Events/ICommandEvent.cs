using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Script.Events
{
    public interface ICommandEvent : IEvent
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        ICommand Command { get; }
    }
}
