using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Events
{
    public interface IInstigatedEvent : IEvent
    {
        /// <summary>
        /// Gets the instigator.
        /// </summary>
        object Instigator { get; }
    }
}
