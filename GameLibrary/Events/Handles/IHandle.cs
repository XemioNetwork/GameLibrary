using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Events.Handles
{
    public interface IHandle
    {
    }

    public interface IHandle<in TEvent> : IHandle where TEvent : IEvent
    {
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        void Handle(TEvent evt);
    }
}
