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

            EventManager eventManager = new EventManager();
            eventManager.Publish(new ExceptionEvent(exception));
        }
        #endregion Private Methods
    }
}
