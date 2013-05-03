using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Script;

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

            ICommand current = this.Command;
            if (current != null)
            {
                current.Execute();
            }

            if (current == null || !current.Active)
            {
                if (!this._enumerator.MoveNext())
                {
                    this._enumerator = null;
                }
            }
        }
        #endregion
    }
}
