using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Config.Validation
{
    public class ValidationException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException" /> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        internal ValidationException(ValidationEntry entry) : base("Validation failed: " + entry.ErrorMessage + ".")
        {
            this.Validator = entry.Validator;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the validator.
        /// </summary>
        public IValidator Validator { get; private set; }
        #endregion
    }
}
