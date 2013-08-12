using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Script
{
    public class CommandEvaluator
    {
        #region Methods
        /// <summary>
        /// Evaluates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        public string Evaluate(string source)
        {
            source = source.Replace("\n", string.Empty);
            source = source.Replace("\r", string.Empty);
            source = source.Replace("\t", " ");

            var implementations = XGL.Components.Get<ImplementationManager>();

            foreach (ICommand command in implementations.All<string, ICommand>())
            {
                int length = command.Id.Length;
                List<int> indices = new List<int>();

                for (int i = 0; i < source.Length - length; i++)
                {
                    string current = source.Substring(i, length);
                    if (current == command.Id)
                    {
                        int openBracket = i + length;

                        if (source[openBracket] == '(')
                        {
                            indices.Add(i);
                        }
                    }
                }

                for (int i = indices.Count - 1; i >= 0; i--)
                {
                    int index = indices[i];

                    source = source.Insert(index + length, "Command");
                    source = source.Insert(index, "yield return new ");
                }
            }

            return source;
        }
        #endregion
    }
}
