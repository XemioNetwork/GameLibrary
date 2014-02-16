using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Syncput.Packages.Requests
{
    public class MessageRequest : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRequest"/> class.
        /// </summary>
        public MessageRequest()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRequest"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MessageRequest(string message)
        {
            this.Message = message;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }
        #endregion
    }
}
