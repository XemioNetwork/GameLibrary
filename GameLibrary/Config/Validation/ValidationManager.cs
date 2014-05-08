using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Logging;

namespace Xemio.GameLibrary.Config.Validation
{
    public class ValidationManager
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationManager"/> class.
        /// </summary>
        public ValidationManager()
        {
            this._validators = new List<ValidationEntry>();
        }
        #endregion

        #region Fields
        private readonly IList<ValidationEntry> _validators; 
        #endregion

        #region Internal Methods
        /// <summary>
        /// Validates all installed validators.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        /// <exception cref="Xemio.GameLibrary.Config.Validation.ValidationException"></exception>
        internal void Validate(ValidationScope scope, Configuration configuration, IComponentCatalog catalog)
        {
            var validators = this._validators.Where(entry => entry.Scope == scope);

            foreach (ValidationEntry entry in validators)
            {
                if (!entry.Validator.Validate(configuration, catalog))
                {
                    throw new ValidationException(entry);
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Specifies a condition that should be fulfilled.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="scope">The scope.</param>
        public void Condition(IValidator validator, ValidationScope scope)
        {
            this._validators.Add(new ValidationEntry(validator, scope));
        }
        /// <summary>
        /// Specifies a condition that should be fulfilled.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="errorMessage">The error error message.</param>
        /// <param name="scope">The scope.</param>
        public void Condition(IValidator validator, string errorMessage, ValidationScope scope)
        {
            this._validators.Add(new ValidationEntry(validator, errorMessage, scope));
        }
        /// <summary>
        /// Specifies a condition that should be fulfilled.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="scope">The scope.</param>
        public void Condition(Expression<Func<Configuration, IComponentCatalog, bool>> function, ValidationScope scope)
        {
            this.Condition(new FunctionValidator(function), scope);
        }
        /// <summary>
        /// Specifies a condition that should be fulfilled.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="scope">The scope.</param>
        public void Condition(Expression<Func<Configuration, IComponentCatalog, bool>> function, string errorMessage, ValidationScope scope)
        {
            this.Condition(new FunctionValidator(function), errorMessage, scope);
        }
        #endregion
    }
}
