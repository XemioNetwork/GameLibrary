using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Logging.Providers;

namespace Xemio.GameLibrary.Logging
{
    public static class LogManager
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="LogManager"/> class.
        /// </summary>
        static LogManager()
        {
            MinLevel = LogLevel.Debug;
            Provider = new ConsoleLoggingProvider();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the minimum level.
        /// </summary>
        public static LogLevel MinLevel { get; set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public static ILoggingProvider Provider { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified level is enabled.
        /// </summary>
        /// <param name="level">The level.</param>
        public static bool IsEnabled(LogLevel level)
        {
            return (int)level >= (int)MinLevel;
        }
        /// <summary>
        /// Gets the logger for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public static Logger GetLogger(Type type)
        {
            return new Logger(type);
        }
        /// <summary>
        /// Gets the current class logger.
        /// </summary>
        public static Logger GetCurrentClassLogger()
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;

            //TODO: Implement support for NLog and log4net.
            return new Logger(type);
        }
        #endregion
    }
}
