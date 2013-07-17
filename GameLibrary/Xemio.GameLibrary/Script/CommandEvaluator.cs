using System.Collections.Generic;
using System.Linq;

namespace Xemio.GameLibrary.Script
{
    public class CommandEvaluator
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandEvaluator"/> class.
        /// </summary>
        public CommandEvaluator()
        {
            this.Commands = new List<ICommand>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandEvaluator"/> class.
        /// </summary>
        /// <param name="commands">The commands.</param>
        public CommandEvaluator(IEnumerable<ICommand> commands)
        {
            this.Commands = commands.ToList();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the commands.
        /// </summary>
        public List<ICommand> Commands { get; private set; }
        #endregion

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

            foreach (ICommand command in this.Commands)
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
