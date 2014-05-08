using System;

namespace Xemio.GameLibrary.Logging.Providers
{
    public interface ILoggingProvider
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        void Log(Type type, LogLevel level, string message);
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        void LogException(Type type, LogLevel level, string message, Exception ex);
    }
}
