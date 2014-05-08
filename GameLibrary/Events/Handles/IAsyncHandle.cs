using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Script;

namespace Xemio.GameLibrary.Events.Handles
{
    public interface IAsyncHandle<in TEvent> : IHandle where TEvent : IEvent
    {
        /// <summary>
        /// Handles the specified event asynchronously.
        /// </summary>
        /// <param name="evt">The event.</param>
        Task Handle(TEvent evt);
    }
}
