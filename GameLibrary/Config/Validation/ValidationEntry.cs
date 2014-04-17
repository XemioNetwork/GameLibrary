using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Config.Validation
{
    internal class ValidationEntry
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationEntry" /> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="scope">The scope.</param>
        public ValidationEntry(IValidator validator, ValidationScope scope)
        {
            this.Validator = validator;
            this.Scope = scope;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationEntry" /> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="scope">The scope.</param>
        public ValidationEntry(IValidator validator, string errorMessage, ValidationScope scope)
        {
            this.Validator = validator;
            this.Scope = scope;

            this._errorMessage = errorMessage;
        }
        #endregion

        #region Fields
        private readonly string _errorMessage;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the validator.
        /// </summary>
        public IValidator Validator { get; private set; }
        /// <summary>
        /// Gets the scope.
        /// </summary>
        public ValidationScope Scope { get; private set; }
        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                if (!string.IsNullOrEmpty(this._errorMessage))
                {
                    return this._errorMessage;
                }

                return this.Validator.DefaultMessage;
            }
        }
        #endregion
    }
}
