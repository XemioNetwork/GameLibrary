using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Events
{
    public class EventManager : IComponent, IObservable<IEvent>
    {
        #region Fields
        private readonly List<dynamic> _observers = new List<dynamic>();
        private readonly Dictionary<Type, List<dynamic>> _typeMappings = new Dictionary<Type, List<dynamic>>(); 
        #endregion

        #region Private Methods
        /// <summary>
        /// Removes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        private void Remove<TEvent>(IObserver<TEvent> observer)
        {
            lock (this._observers)
            {
                this._observers.Remove(observer);
            }
            lock (this._typeMappings)
            {
                foreach (Type type in ReflectionCache.GetInheritedTypes(typeof(TEvent)))
                {
                    if (this._typeMappings.ContainsKey(type))
                    {
                        this._typeMappings[type].Remove(observer);
                    }

                }
            }

            observer.OnCompleted();
        }
        /// <summary>
        /// Determines whether the specified observer is observing the specified type.
        /// </summary>
        /// <param name="observer">The observer.</param>
        private bool IsObserver<TEvent>(dynamic observer)
        {
            Type type = observer.GetType();
            Type targetType = typeof(TEvent);
            Type genericType = ReflectionCache.GetGenericArguments(type).First();

            return genericType.IsAssignableFrom(targetType);
        }
        /// <summary>
        /// Gets the observers.
        /// </summary>
        private IEnumerable<dynamic> GetObservers<T>() where T : class, IEvent
        {
            Type type = typeof(T);

            lock (this._typeMappings)
            {
                if (!this._typeMappings.ContainsKey(type))
                {
                    lock (this._observers)
                    {
                        this._typeMappings.Add(type, new List<dynamic>(this._observers.Where(o => this.IsObserver<T>(o))));
                    }
                }

                return this._typeMappings[type];
            }
        }
        #endregion Private Methods

        #region Methods
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="e">The event.</param>
        public void Publish<TEvent>(TEvent e) where TEvent : class, IEvent
        {
            var interceptable = e as IInterceptableEvent;
            foreach (IObserver<TEvent> observer in this.GetObservers<TEvent>())
            {
                try
                {
                    observer.OnNext(e);
                    if (interceptable != null && interceptable.IsCanceled)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            }
        }
        /// <summary>
        /// Subscribes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public IDisposable Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            return this.Subscribe(new ActionObserver<T>(action));
        }
        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="observer">The observer.</param>
        public IDisposable Subscribe<T>(IObserver<T> observer) where T : class, IEvent
        {
            lock (this._observers)
            {
                this._observers.Add(observer);
            }

            return new ActionDisposable(() => this.Remove(observer));
        }
        #endregion

        #region Implementation of IObservable<IEvent>
        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        IDisposable IObservable<IEvent>.Subscribe(IObserver<IEvent> observer)
        {
            var interfaces = ReflectionCache.GetInterfaces(observer.GetType())
                .Where(i => typeof(IObservable<>).IsAssignableFrom(i) && i.IsGenericType);

            foreach (Type interfaceType in interfaces)
            {
                Type genericType = ReflectionCache.GetGenericArguments(interfaceType).First();

                MethodInfo subscribeMethod = ReflectionCache
                    .GetMethods(this.GetType())
                    .Where(method => method.Name == "Subscribe" && method.IsGenericMethod)
                    .Single(method => ReflectionCache.GetParameters(method).First().Name == "observer");

                subscribeMethod.MakeGenericMethod(genericType).Invoke(this, new[] {observer});
            }

            return new ActionDisposable(() => this.Remove(observer));
        }
        #endregion
    }
}
