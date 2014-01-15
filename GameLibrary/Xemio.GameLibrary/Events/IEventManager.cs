using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Events
{
    [AbstractComponent]
    public interface IEventManager : IComponent, IObservable<IEvent>
    {
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="e">The event.</param>
        void Publish<TEvent>(TEvent e) where TEvent : class, IEvent;
        /// <summary>
        /// Subscribes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        IDisposable Subscribe<T>(Action<T> action) where T : class, IEvent;
        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="observer">The observer.</param>
        IDisposable Subscribe<T>(IObserver<T> observer) where T : class, IEvent;
        /// <summary>
        /// Loads the IEvent implementations from the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        void LoadEventsFrom(IAssemblyContext context);
        /// <summary>
        /// Loads the IEvent implementations from the assembly of the specified type.
        /// </summary>
        void LoadEventsFromAssemblyOf(Type type);
        /// <summary>
        /// Loads the IEvent implementations from the assembly of the specified type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        void LoadEventsFromAssemblyOf<T>();
    }
}
