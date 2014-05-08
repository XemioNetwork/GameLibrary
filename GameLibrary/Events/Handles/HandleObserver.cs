using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Events.Handles
{
    public class HandleObserver : IObserver<IEvent>
    {
        #region Implementation of IObserver<in IEvent>
        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="evt">The event.</param>
        public void OnNext(IEvent evt)
        {
            foreach (IComponent component in XGL.Components)
            {
                Handle.Send(component, evt);
            }
        }
        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
        }
        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
        }
        #endregion
    }
}
