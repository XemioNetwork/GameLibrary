using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.Exceptions;

namespace Xemio.GameLibrary.Content.Layouts.Exceptions
{
    public class ReferenceNotFoundException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceNotFoundException"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="innerException">The inner exception.</param>
        public ReferenceNotFoundException(string tag, Guid guid, GuidNotFoundException innerException)
            : base("Reference not found: Guid " + guid.ToString("D") + " was referenced for field " + tag +
                   ". Make sure a file providing the specified guid inside its .meta file does exist.", innerException)
        {
            this.Tag = tag;
            this.Guid = guid;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Guid { get; private set; }
        #endregion
    }
}
