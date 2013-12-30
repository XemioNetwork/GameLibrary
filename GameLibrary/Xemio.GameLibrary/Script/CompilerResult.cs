using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerResult" /> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public CompilerResult(Assembly assembly)
        {
            this.Errors = new List<CompilerError>();
            this.Assembly = assembly;
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
        /// Gets the assembly.
        /// </summary>
        public Assembly Assembly { get; private set; }
        #endregion
    }
}
