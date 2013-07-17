using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Script.Events
{
    public class ExecutingCommandEvent : ICommandEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutedCommandEvent"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        public ExecutingCommandEvent(ICommand command)
        {
            this.Command = command;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the command.
        /// </summary>
        public ICommand Command { get; private set; }
        #endregion
    }
}
