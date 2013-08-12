using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.GameLibrary.Common
{
    public class GlobalExceptionHandler : IConstructable
    {
        #region Fields
        /// <summary>
        /// The last exception that was thrown.
        /// Needed because for some reason the UnhandledException event will be called twice.
        /// </summary>
        private Exception _lastException;
        #endregion Fields

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            AppDomain.CurrentDomain.UnhandledException += HandleException;
        }
        #endregion IConstructable Member

        #region Private Methods
        /// <summary>
        /// Handles all unhandled Exceptions and fires an ExceptionEvent.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;

            if (exception != this._lastException)
            { 
                EventManager eventManager = XGL.Components.Get<EventManager>();
                eventManager.Publish(new ExceptionEvent(exception));

                this._lastException = exception;
            }
        }
        #endregion Private Methods
    }
}
