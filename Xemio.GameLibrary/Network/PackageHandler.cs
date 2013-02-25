using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network
{
    public class PackageHandler : IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageHandler"/> class.
        /// </summary>
        public PackageHandler()
        {
            this._subscribers = new Dictionary<Type, ActionCollection<Package>>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, ActionCollection<Package>> _subscribers;
        #endregion

        #region Methods
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="packageEvent">The event.</param>
        protected void HandleEvent(ReceivedPackageEvent packageEvent)
        {
            Type type = packageEvent.Package.GetType();
            if (this._subscribers.ContainsKey(type))
            {
                foreach (Action<Package> action in this._subscribers[type])
                {
                    action(packageEvent.Package);
                }
            }
        }
        /// <summary>
        /// Subscribes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="action">The action.</param>
        public void Subscribe(Type type, Action<Package> action)
        {
            if (!this._subscribers.ContainsKey(type))
            {
                this._subscribers.Add(type, new ActionCollection<Package>());
            }

            this._subscribers[type].Add(action);
        }
        /// <summary>
        /// Subscribes the specified action to arriving packages of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        public void Subscribe<T>(Action<T> action) where T : Package
        {
            this.Subscribe(typeof(T), package => action(package as T));
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            XGL.GetComponent<EventManager>()
                .Subscribe<ReceivedPackageEvent>(this.HandleEvent);
        }
        #endregion
    }
}
