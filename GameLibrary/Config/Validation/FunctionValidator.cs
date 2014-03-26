using System;
using System.Linq.Expressions;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Config.Validation
{
    public class FunctionValidator : IValidator
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionValidator"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public FunctionValidator(Expression<Func<Configuration, IComponentCatalog, bool>> expression)
        {
            this._body = Lambdas.ToString(expression);
            this._function = expression.Compile();
        }
        #endregion

        #region Fields
        private readonly string _body;
        private readonly Func<Configuration, IComponentCatalog, bool> _function; 
        #endregion

        #region Implementation of ICondition
        /// <summary>
        /// Gets the default message.
        /// </summary>
        public string DefaultMessage
        {
            get { return "Unknown validation error caused by " + this._body + "."; }
        }
        /// <summary>
        /// Gets a value indicating whether the expression is fulfilled.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public bool Validate(Configuration configuration, IComponentCatalog catalog)
        {
            return this._function(configuration, catalog);
        }
        #endregion
    }
}
