using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Exceptions
{
    public class DuplicateGuidException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateGuidException"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public DuplicateGuidException(Guid guid) : base("Invalid meta data files: Multiple files contain the same guid " + guid.ToString("D") + ".")
        {
            this.Guid = guid;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Guid { get; private set; }
        #endregion
    }
}
