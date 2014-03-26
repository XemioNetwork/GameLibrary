using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Config.Validation
{
    internal class InversiveValidator : IValidator
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InversiveValidator"/> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        public InversiveValidator(IValidator validator)
        {
            this._validator = validator;
        }
        #endregion

        #region Fields
        private readonly IValidator _validator;
        #endregion

        #region Implementation of IValidator
        /// <summary>
        /// Gets the default message.
        /// </summary>
        public string DefaultMessage
        {
            get { return this._validator.DefaultMessage; }
        }
        /// <summary>
        /// Gets a value indicating whether the condition is fulfilled.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public bool Validate(Configuration configuration, IComponentCatalog catalog)
        {
            return !this._validator.Validate(configuration, catalog);
        }
        #endregion
    }
}
