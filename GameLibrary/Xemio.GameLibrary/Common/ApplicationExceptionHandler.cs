using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Common
{
    public class ApplicationExceptionHandler : IConstructable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        /// <summary>
        /// The last exception that was thrown.
        /// Needed because for some reason the UnhandledException event will be called twice.
        /// </summary>
        private Exception _lastException;
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            AppDomain.CurrentDomain.UnhandledException += HandleException;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Handles all unhandled Exceptions and fires an ExceptionEvent.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;

            if (exception != this._lastException)
            { 
                logger.Error(exception);
                this._lastException = exception;
            }
        }
        #endregion
    }
}
