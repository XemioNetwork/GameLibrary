using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Script
{
    public class CompilerError
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerError"/> class.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="message">The message.</param>
        public CompilerError(int line, string message)
        {
            this.Line = line;
            this.Message = message;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the line.
        /// </summary>
        public int Line { get; private set; }
        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }
        #endregion
    }
}
