using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Exceptions
{
    public class GuidNotFoundException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidNotFoundException"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public GuidNotFoundException(Guid guid) : base("Guid " + guid.ToString("D") + " not found.")
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
