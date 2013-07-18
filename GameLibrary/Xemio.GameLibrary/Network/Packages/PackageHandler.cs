using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Packages
{
    public class PackageHandler : IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageHandler"/> class.
        /// </summary>
        public PackageHandler()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="packageEvent">The event.</param>
        protected void HandlePackage(ReceivedPackageEvent packageEvent)
        {
        }
        /// <summary>
        /// Subscribes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="action">The action.</param>
        public void Subscribe(Type type, Action<Package> action)
        {
        }
        /// <summary>
        /// Subscribes the specified action to arriving packages of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        public void Subscribe<T>(Action<T> action) where T : Package
        {
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            EventManager eventManager = XGL.Components.Get<EventManager>();
            eventManager.Subscribe<ReceivedPackageEvent>(this.HandlePackage);
        }
        #endregion
    }
}
