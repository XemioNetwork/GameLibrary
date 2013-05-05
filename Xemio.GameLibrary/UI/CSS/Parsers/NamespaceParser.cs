using System;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.UI.CSS.Expressions;
using Xemio.GameLibrary.UI.CSS.Namespaces;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.CSS.Parsers
{
    public class NamespaceParser : IParser<string, IEnumerable<IExpression>>
    {
        #region Methods
        /// <summary>
        /// Validates the type instance.
        /// </summary>
        /// <param name="typeInstance">The type instance.</param>
        /// <param name="expression">The expression.</param>
        private void ValidateTypeInstance(IExpression typeInstance, string expression)
        {
            if (typeInstance == null)
            {
                const string format = "Invalid namespace member '{0}'.";
                string message = string.Format(format, expression);

                throw new InvalidOperationException(message);
            }
        }
        #endregion

        #region Implementation of IParser
        /// <summary>
        /// Parses the specified namespace.
        /// </summary>
        /// <param name="input">The input.</param>
        public IEnumerable<IExpression> Parse(string input)
        {
            var linker = new AutomaticLinker<string, IExpression>();
            linker.CreationType = CreationType.CreateNew;
            
            string[] expressions = input.Split(' ');
            foreach (string expression in expressions)
            {
                //Seperate state from expression
                string[] parts = expression.Split(':');

                //Get expression and state
                string expressionName = parts.FirstOrDefault();
                string state = parts.Length > 1 ? parts[1] : "None";

                IExpression typeInstance = linker.FirstOrDefault(
                    ex => ex.IsExpression(expression));

                if (!string.IsNullOrEmpty(expressionName))
                {
                    this.ValidateTypeInstance(typeInstance, expression);

                    object stateEnum = Enum.Parse(typeof(WidgetState), state);
                    IExpression instance = linker.Resolve(typeInstance.Identifier);

                    instance.State = (WidgetState)stateEnum;
                    instance.Parse(expressionName);

                    yield return instance;
                }
            }
        } 
        #endregion
    }
}
