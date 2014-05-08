using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Logging.Providers
{
    public class ConsoleLoggingProvider : ILoggingProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLoggingProvider"/> class.
        /// </summary>
        public ConsoleLoggingProvider()
        {
            this.Colors = new []
            {
                ConsoleColor.DarkGray,
                ConsoleColor.DarkGray,
                ConsoleColor.White,
                ConsoleColor.Yellow,
                ConsoleColor.Red,
                ConsoleColor.DarkRed
            };
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        public ConsoleColor[] Colors { get; set; }
        #endregion

        #region Implementation of ILoggingProvider
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        public void Log(Type type, LogLevel level, string message)
        {
            Console.ForegroundColor = this.Colors[(int)level];
            Console.WriteLine("[" + DateTime.Now + "] [" + type + "] [" + level.ToString().ToUpper() + "] [" + message + "]");
        }
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void LogException(Type type, LogLevel level, string message, Exception ex)
        {
            this.Log(type, level, message);
            
            Console.WriteLine(ex.GetType());
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
        #endregion
    }
}
