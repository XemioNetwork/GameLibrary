using System;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.UI.CSS.Namespaces;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.CSS.Parsers
{
    public class NamespaceParser : IParser<IEnumerable<INamespaceExpression>>
    {
        #region Implementation of IParser
        /// <summary>
        /// Parses the specified namespace.
        /// </summary>
        /// <param name="input">The input.</param>
        public IEnumerable<INamespaceExpression> Parse(string input)
        {
            GenericLinker<string, INamespaceExpression> linker = new AutomaticLinker<string, INamespaceExpression>();
            linker.CreationType = CreationType.Instantiate;
            
            string[] expressions = input.Split(' ');
            foreach (string expression in expressions)
            {
                string[] parts = expression.Split(':');

                string expressionName = parts.FirstOrDefault();
                string state = parts.Length > 1 ? parts[1] : "None";
                
                INamespaceExpression typeInstance = linker.FirstOrDefault(
                    ex => ex.IsExpression(expression));

                if (typeInstance == null)
                {
                    string format = "Invalid namespace member '{0}'.";
                    string message = string.Format(format, expression);

                    throw new InvalidOperationException(message);
                }

                INamespaceExpression instance = linker.Resolve(typeInstance.Identifier);
                instance.State = (WidgetState)Enum.Parse(typeof(WidgetState), state);
                instance.Parse(expressionName);

                yield return instance;
            }
        } 
        #endregion
    }
}
