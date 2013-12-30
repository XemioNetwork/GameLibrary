using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Formats.Fallback
{
    public class FallbackException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackException" /> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public FallbackException(Exception innerException)
            : base("The original format could not be initialized and fallback took place. Make sure your file format is valid.", innerException)
        {
        }
        #endregion
    }
}
