using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Logging.Providers;

namespace Xemio.GameLibrary.Logging
{
    public class Logger
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        internal Logger(Type type)
        {
            this.Type = type;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Logs the specified message as trace.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Trace(string message)
        {
            this.Log(LogLevel.Trace, message);
        }
        /// <summary>
        /// Logs the specified message as debug.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            this.Log(LogLevel.Debug, message);
        }
        /// <summary>
        /// Logs the specified message as info.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            this.Log(LogLevel.Info, message);
        }
        /// <summary>
        /// Logs the specified message as warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            this.Log(LogLevel.Warn, message);
        }
        /// <summary>
        /// Logs the specified message as error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            this.Log(LogLevel.Error, message);
        }
        /// <summary>
        /// Logs the specified message as fatal.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(string message)
        {
            this.Log(LogLevel.Fatal, message);
        }
        /// <summary>
        /// Logs the specified message as trace.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void Trace(string format, params object[] parameters)
        {
            this.Log(LogLevel.Trace, string.Format(format, parameters));
        }
        /// <summary>
        /// Logs the specified message as debug.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void Debug(string format, params object[] parameters)
        {
            this.Log(LogLevel.Debug, string.Format(format, parameters));
        }
        /// <summary>
        /// Logs the specified message as info.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void Info(string format, params object[] parameters)
        {
            this.Log(LogLevel.Info, string.Format(format, parameters));
        }
        /// <summary>
        /// Logs the specified message as warning.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void Warn(string format, params object[] parameters)
        {
            this.Log(LogLevel.Warn, string.Format(format, parameters));
        }
        /// <summary>
        /// Logs the specified message as error.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void Error(string format, params object[] parameters)
        {
            this.Log(LogLevel.Error, string.Format(format, parameters));
        }
        /// <summary>
        /// Logs the specified message as fatal.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void Fatal(string format, params object[] parameters)
        {
            this.Log(LogLevel.Fatal, string.Format(format, parameters));
        }
        /// <summary>
        /// Logs the specified message as trace.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void TraceException(string message, Exception ex)
        {
            this.LogException(LogLevel.Trace, message, ex);
        }
        /// <summary>
        /// Logs the specified message as debug.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void DebugException(string message, Exception ex)
        {
            this.LogException(LogLevel.Debug, message, ex);
        }
        /// <summary>
        /// Logs the specified message as info.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void InfoException(string message, Exception ex)
        {
            this.LogException(LogLevel.Info, message, ex);
        }
        /// <summary>
        /// Logs the specified message as warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void WarnException(string message, Exception ex)
        {
            this.LogException(LogLevel.Warn, message, ex);
        }
        /// <summary>
        /// Logs the specified message as error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void ErrorException(string message, Exception ex)
        {
            this.LogException(LogLevel.Error, message, ex);
        }
        /// <summary>
        /// Logs the specified message as fatal.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void FatalException(string message, Exception ex)
        {
            this.LogException(LogLevel.Fatal, message, ex);
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        protected void Log(LogLevel level, string message)
        {
            if (LogManager.IsEnabled(level))
            {
                LogManager.Provider.Log(this.Type, level, message);
            }
        }
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        protected void LogException(LogLevel level, string message, Exception ex)
        {
            if (LogManager.IsEnabled(level))
            {
                LogManager.Provider.LogException(this.Type, level, message, ex);
            }
        }
        #endregion
    }
}
