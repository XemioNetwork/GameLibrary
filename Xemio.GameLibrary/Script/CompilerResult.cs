using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Script
{
    public class CompilerResult
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerResult"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public CompilerResult(IEnumerable<CompilerError> errors)
        {
            this.Errors = errors.ToList();
            this.Scripts = new ScriptCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerResult"/> class.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        public CompilerResult(IEnumerable<IScript> scripts)
        {
            this.Errors = new List<CompilerError>();
            this.Scripts = new ScriptCollection(scripts);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="CompilerResult"/> was successful.
        /// </summary>
        public bool Succeed
        {
            get { return this.Errors.Count == 0; }
        }
        /// <summary>
        /// Gets the errors.
        /// </summary>
        public List<CompilerError> Errors { get; private set; }
        /// <summary>
        /// Gets the scripts.
        /// </summary>
        public ScriptCollection Scripts { get; private set; }
        #endregion
    }
}
