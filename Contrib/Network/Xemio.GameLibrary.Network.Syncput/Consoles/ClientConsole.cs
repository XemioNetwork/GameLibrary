using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Syncput.Packages.Requests;

namespace Xemio.GameLibrary.Network.Syncput.Consoles
{
    public class ClientConsole : IConsole
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConsole"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="capacity">The capacity.</param>
        public ClientConsole(IClient client, int capacity)
        {
            this._client = client;
            this._messages = new CachedList<string>();

            this.Capacity = capacity;
        }
        #endregion

        #region Fields
        private readonly IClient _client;
        private readonly CachedList<string> _messages;
        #endregion
        
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
            return this._messages;
        }
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="message">The message.</param>
        public void Send(string playerName, string message)
        {
            this._client.Send(new MessageRequest(message));
        }
        /// <summary>
        /// Sends the formatted message.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void SendFormatted(string playerName, string format, params object[] parameters)
        {
            this.Send(playerName, string.Format(format, parameters));
        }
        /// <summary>
        /// Writes a line.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteLine(string message)
        {
            if (this._messages.Count >= this.Capacity)
                this._messages.RemoveAt(0);

            this._messages.Add(message);
        }
        /// <summary>
        /// Writes a formatted line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public void WriteFormattedLine(string format, params object[] parameters)
        {
            this.WriteLine(string.Format(format, parameters));
        }
        #endregion
    }
}
