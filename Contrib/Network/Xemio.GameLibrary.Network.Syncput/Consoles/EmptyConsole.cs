using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Syncput.Consoles
{
    public class EmptyConsole : IConsole
    {
        #region Implementation of IConsole
        /// <summary>
        /// Gets or sets the capacity.
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// Gets the messages.
        /// </summary>
        public IList<string> GetMessages()
        {
            return new List<string>();
        }
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="message">The message.</param>
        public void Send(string playerName, string message)
        {
        }
        /// <summary>
        /// Sends the formatted message.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void SendFormatted(string playerName, string format, params object[] parameters)
        {
        }
        /// <summary>
        /// Writes a line.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteLine(string message)
        {
        }
        /// <summary>
        /// Writes a formatted line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void WriteFormattedLine(string format, params object[] parameters)
        {
        }
        #endregion
    }
}
