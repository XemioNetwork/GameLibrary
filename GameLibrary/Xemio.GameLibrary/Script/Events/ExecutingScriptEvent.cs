using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Script.Events
{
    public class ExecutingScriptEvent : IScriptEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutedScriptEvent"/> class.
        /// </summary>
        /// <param name="script">The script.</param>
        public ExecutingScriptEvent(IScript script)
        {
            this.Script = script;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the script.
        /// </summary>
        public IScript Script { get; private set; }
        #endregion
    }
}
