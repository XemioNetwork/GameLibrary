using System.Threading.Tasks;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Script
{
    public interface IAsyncHandler<in TEvent> : IHandler where TEvent : IEvent
    {
        /// <summary>
        /// Executes the script for the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        Task Execute(TEvent evt);
    }
}