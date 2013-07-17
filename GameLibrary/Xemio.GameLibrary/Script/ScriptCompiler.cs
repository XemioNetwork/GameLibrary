using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Script;

namespace Xemio.GameLibrary.Script
{
    using CodeDom = System.CodeDom.Compiler;

    public class ScriptCompiler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptCompiler"/> class.
        /// </summary>
        /// <param name="commands">The commands.</param>
        public ScriptCompiler(IEnumerable<ICommand> commands)
        {
            this.Assemblies = new List<string>();

            this.Commands = new GenericLinker<string, ICommand>();
            this.Commands.Add(commands);
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the output assembly.
        /// </summary>
        public string OutputAssembly { get; set; }
        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        public List<string> Assemblies { get; private set; }
        /// <summary>
        /// Gets the commands.
        /// </summary>
        public GenericLinker<string, ICommand> Commands { get; private set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified type has a default constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        private bool HasDefaultConstructor(Type type)
        {
            return type.GetConstructors().Any(c => c.GetParameters().Length == 0);
        }
        /// <summary>
        /// Loads the instances.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        private IEnumerable<IScript> LoadInstances(Assembly assembly)
        {
            List<IScript> instances = new List<IScript>();

            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsGenericType && !type.IsAbstract && this.HasDefaultConstructor(type))
                {
                    if (typeof(IScript).IsAssignableFrom(type))
                    {
                        object instance = Activator.CreateInstance(type);
                        instances.Add(instance as IScript);
                    }
                }
            }

            return instances.ToArray();
        }
        /// <summary>
        /// Creates compiler parameters.
        /// </summary>
        private CodeDom.CompilerParameters CreateParameters()
        {
            CodeDom.CompilerParameters parameters = new CodeDom.CompilerParameters();
            parameters.OutputAssembly = this.OutputAssembly;
            parameters.GenerateInMemory = false;

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.AddRange(this.Assemblies.ToArray());

            return parameters;
        }
        /// <summary>
        /// Compiles the specified sources.
        /// </summary>
        /// <param name="sources">The sources.</param>
        public CompilerResult Compile(params string[] sources)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CommandEvaluator evaluator = new CommandEvaluator(this.Commands);

            string[] evaluatedSources = sources
                .Select(evaluator.Evaluate)
                .ToArray();

            CodeDom.CompilerResults result = codeProvider.CompileAssemblyFromSource(
                this.CreateParameters(), evaluatedSources);

            bool succeed = result.Errors.Count == 0;
            List<CompilerError> errors = new List<CompilerError>();

            if (!succeed)
            {
                foreach (CodeDom.CompilerError error in result.Errors)
                {
                    errors.Add(new CompilerError(error.Line, error.ErrorText));
                }

                return new CompilerResult(errors);
            }

            IEnumerable<IScript> scripts = this.LoadInstances(result.CompiledAssembly);
            return new CompilerResult(scripts);
        }
        #endregion
    }
}
