using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NLog;
using Xemio.GameLibrary.Components;

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
        public void Condition(IValidator validator)
        {
            this._validators.Add(new ValidationEntry(validator));
        }
        /// <summary>
        /// Specifies a condition that should be fulfilled.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="errorMessage">The error error message.</param>
        public void Condition(IValidator validator, string errorMessage)
        {
            this._validators.Add(new ValidationEntry(validator, errorMessage));
        }
        /// <summary>
        /// Specifies a condition that should not be fulfilled.
        /// </summary>
        /// <param name="validator">The validator.</param>
        public void Not(IValidator validator)
        {
            this.Condition(new InversiveValidator(validator));
        }
        /// <summary>
        /// Specifies a condition that should not be fulfilled.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <param name="errorMessage">The error message.</param>
        public void Not(IValidator validator, string errorMessage)
        {
            this.Condition(new InversiveValidator(validator), errorMessage);
        }
        /// <summary>
        /// Specifies a condition that should be fulfilled.
        /// </summary>
        /// <param name="function">The function.</param>
        public void Condition(Expression<Func<Configuration, IComponentCatalog, bool>> function)
        {
            this.Condition(new FunctionValidator(function));
        }
        /// <summary>
        /// Specifies a condition that should be fulfilled.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="errorMessage">The error message.</param>
        public void Condition(Expression<Func<Configuration, IComponentCatalog, bool>> function, string errorMessage)
        {
            this.Condition(new FunctionValidator(function), errorMessage);
        }
        /// <summary>
        /// Specifies a condition that should not be fulfilled.
        /// </summary>
        /// <param name="function">The function.</param>
        public void Not(Expression<Func<Configuration, IComponentCatalog, bool>> function)
        {
            this.Not(new FunctionValidator(function));
        }
        /// <summary>
        /// Specifies a condition that should not be fulfilled.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="errorMessage">The error message.</param>
        public void Not(Expression<Func<Configuration, IComponentCatalog, bool>> function, string errorMessage)
        {
            this.Not(new FunctionValidator(function), errorMessage);
        }
        #endregion
    }
}
