using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Script
{
    public interface IHandler
    {
    }
    public interface IHandler<in TEvent> : IHandler where TEvent : IEvent
    {
        /// <summary>
        /// Executes the script for the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        void Execute(TEvent evt);
    }
    public interface IAsyncHandler<in TEvent> : IHandler where TEvent : IEvent
    {
        /// <summary>
        /// Executes the script for the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        Task Execute(TEvent evt);
    }
}
