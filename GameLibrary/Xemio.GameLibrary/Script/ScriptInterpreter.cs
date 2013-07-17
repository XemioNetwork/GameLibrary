using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Script;
using Xemio.GameLibrary.Script.Events;

namespace Xemio.GameLibrary.Script
{
    public class ScriptInterpreter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInterpreter"/> class.
        /// </summary>
        public ScriptInterpreter()
        {
        }
        #endregion

        #region Fields
        private IEnumerator _enumerator;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the command.
        /// </summary>
        protected ICommand Command
        {
            get { return this._enumerator.Current as ICommand; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the specified script.
        /// </summary>
        /// <param name="script">The script.</param>
        public void Run(IScript script)
        {
            IEnumerable enumerable = script.Execute();

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Publish(new ExecutingScriptEvent(script));

            this._enumerator = enumerable.GetEnumerator();
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (this._enumerator == null)
                return;

            EventManager eventManager = XGL.GetComponent<EventManager>();
            ICommand current = this.Command;

            if (current != null)
            {
                current.Execute();
                if (!current.Active)
                {
                    eventManager.Publish(new ExecutedCommandEvent(current));
                }
            }

            if (current == null || !current.Active)
            {
                if (this._enumerator.MoveNext())
                {
                    eventManager.Publish(new ExecutingCommandEvent(this.Command));
                    return;
                }

                this._enumerator = null;
            }
        }
        #endregion
    }
}
