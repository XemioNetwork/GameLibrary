using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Events
{
    public interface IInterceptableEvent : IEvent
    {
        /// <summary>
        /// Gets a value indicating whether the event propagation was canceled.
        /// </summary>
        bool IsCanceled { get; }
        /// <summary>
        /// Cancels the event propagation.
        /// </summary>
        void Cancel();
    }
}
