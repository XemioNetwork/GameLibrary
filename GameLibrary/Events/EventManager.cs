using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events.Handles;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Plugins.Contexts;

namespace Xemio.GameLibrary.Events
{
    public class EventManager : IEventManager, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventManager"/> class.
        /// </summary>
        public EventManager()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EventManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EventManager(IAssemblyContext context)
        {
            this.LoadEventsFrom(context);
            this.Subscribe(new HandleObserver());
        }
        #endregion

        #region Fields
        private readonly List<dynamic> _observers = new List<dynamic>();
        private readonly Dictionary<Type, ProtectedList<dynamic>> _observerMappings = new Dictionary<Type, ProtectedList<dynamic>>();
        /// <summary>
        /// A mapping from a base type to it's super types.
        /// We use this dictionary for faster subscribe times, since we already know what type-mappings we need.
        /// </summary>
        private readonly Dictionary<Type, HashSet<Type>> _typeInheritanceMappings = new Dictionary<Type, HashSet<Type>>(); 
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
            lock (this._observerMappings)
            {
                foreach (Type type in Reflection.GetInheritedTypes(typeof(TEvent)))
                {
                    if (this._observerMappings.ContainsKey(type))
                    {
                        this._observerMappings[type].Remove(observer);
                    }

                }
            }

            observer.OnCompleted();
        }
        /// <summary>
        /// Adds the <paramref name="superType"/> to the super types of <paramref name="baseType"/>.
        /// </summary>
        /// <param name="baseType">The base type..</param>
        /// <param name="superType">The super type.</param>
        private void AddSuperType(Type baseType, Type superType)
        {
            if (this._typeInheritanceMappings.ContainsKey(baseType) == false)
                this._typeInheritanceMappings.Add(baseType, new HashSet<Type>());

            HashSet<Type> superTypes = this._typeInheritanceMappings[baseType];
            superTypes.Add(superType);
        }
        /// <summary>
        /// Adds the specified observer for it's event super types.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="observer">The observer.</param>
        private void AddObserverForSuperTypes<T>(IObserver<T> observer) where T : class, IEvent
        {
            HashSet<Type> superTypes = this._typeInheritanceMappings[typeof(T)];
            foreach (Type superType in superTypes)
            {
                lock (this._observerMappings)
                {
                    if (this._observerMappings.ContainsKey(superType))
                    {
                        this._observerMappings[superType].Add(observer);
                    }
                }
            }
        }
        #endregion Private Methods

        #region Implementation of IEventManager
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="e">The event.</param>
        public void Publish<TEvent>(TEvent e) where TEvent : class, IEvent
        {
            lock (this._observerMappings)
            {
                if (!this._observerMappings.ContainsKey(typeof(TEvent)))
                {
                    this.LoadEventsFromAssemblyOf<TEvent>();
                    if (e.GetType() != typeof(TEvent))
                    {
                        this.LoadEventsFromAssemblyOf(e.GetType());
                    }

                    if (!this._observerMappings.ContainsKey(typeof(TEvent)))
                    {
                        throw new InvalidOperationException("Could not resolve event inheritance tree for " + typeof(TEvent) + ".");
                    }
                }

                var interceptable = e as ICancelableEvent;
                foreach (IObserver<TEvent> observer in this._observerMappings[typeof(TEvent)])
                {
                    try
                    {
                        observer.OnNext(e);

                        if (interceptable != null && interceptable.IsCanceled)
                            break;
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                    }
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

            lock (this._typeInheritanceMappings)
            {
                if (this._typeInheritanceMappings.ContainsKey(typeof(T)) == false)
                {
                    this.LoadEventsFrom(ContextFactory.CreateSingleAssemblyContext(typeof(T).Assembly));
                }

                this.AddObserverForSuperTypes(observer);
            }

            return new ActionDisposable(() => this.Remove(observer));
        }
        /// <summary>
        /// Loads the IEvent implementations from the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        public void LoadEventsFrom(IAssemblyContext context)
        {
            foreach (var assembly in context.Assemblies)
            {
                var eventTypes = from type in assembly.GetTypes()
                                 where type.IsGenericType == false && typeof (IEvent).IsAssignableFrom(type)
                                 select type;

                lock (this._observerMappings)
                { 
                    foreach (Type baseType in eventTypes)
                    {
                        if (this._observerMappings.ContainsKey(baseType) == false)
                            this._observerMappings.Add(baseType, new AutoProtectedList<dynamic>());

                        foreach (Type knownSuperType in this._observerMappings.Keys.Where(baseType.IsAssignableFrom))
                        {
                            this.AddSuperType(baseType, knownSuperType);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Loads the IEvent implementations from the assembly of the specified type.
        /// </summary>
        public void LoadEventsFromAssemblyOf(Type type)
        {
            this.LoadEventsFrom(new SingleAssemblyContext(type.Assembly));
        }
        /// <summary>
        /// Loads the IEvent implementations from the assembly of the specified type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        public void LoadEventsFromAssemblyOf<T>()
        {
            this.LoadEventsFromAssemblyOf(typeof(T));
        }
        #endregion

        #region Implementation of IObservable<IEvent>
        /// <summary>
        /// The subcribe method.
        /// </summary>
        private static MethodInfo subscribeMethod;
        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        IDisposable IObservable<IEvent>.Subscribe(IObserver<IEvent> observer)
        {
            var interfaces = Reflection.GetInterfaces(observer.GetType())
                .Where(i => typeof(IObservable<>).IsAssignableFrom(i) && i.IsGenericType);

            foreach (Type interfaceType in interfaces)
            {
                Type genericType = Reflection.GetGenericArguments(interfaceType).First();

                if (subscribeMethod == null)
                {
                    subscribeMethod = Reflection
                        .GetMethods(this.GetType())
                        .Where(method => method.Name == "Subscribe" && method.IsGenericMethod)
                        .Single(method => Reflection.GetParameters(method).First().Name == "observer");
                }

                subscribeMethod.MakeGenericMethod(genericType).Invoke(this, new[] {observer});
            }

            return new ActionDisposable(() => this.Remove(observer));
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            this.LoadEventsFrom(ContextFactory.CreateApplicationAssemblyContext());
        }
        #endregion
    }
}
