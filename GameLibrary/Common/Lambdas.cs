using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public static class Lambdas
    {
        #region Methods
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public static string ToString(Expression expression)
        {
            return Lambdas.ToString((LambdaExpression)expression);
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public static string ToString(LambdaExpression expression)
        {
            string body = expression.Body.ToString();
            body = body.Replace("AndAlso", "&&");

            return body;
        }
        #endregion
    }
}
