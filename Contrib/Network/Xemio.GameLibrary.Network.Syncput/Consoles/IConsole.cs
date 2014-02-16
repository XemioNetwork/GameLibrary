using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Syncput.Consoles
{
    public interface IConsole
    {
        /// <summary>
        /// Gets or sets the capacity.
        /// </summary>
        int Capacity { get; set; }
        /// <summary>
        /// Gets the messages.
        /// </summary>
        IList<string> GetMessages();
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="message">The message.</param>
        void Send(string playerName, string message);
        /// <summary>
        /// Sends the formatted message.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        void SendFormatted(string playerName, string format, params object[] parameters);
        /// <summary>
        /// Writes a line.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLine(string message);
        /// <summary>
        /// Writes a formatted line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        void WriteFormattedLine(string format, params object[] parameters);
    }
}
