using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibary.Script
{
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
            this.Errors = new List<CompilerError>();

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
        /// Gets a value indicating whether this <see cref="ScriptCompiler"/> is succeed.
        /// </summary>
        public bool Succeed { get; private set; }
        /// <summary>
        /// Gets the errors.
        /// </summary>
        public List<CompilerError> Errors { get; private set; } 
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
        private IScript[] LoadInstances(Assembly assembly)
        {
            List<IScript> instances = new List<IScript>();

            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsGenericType && !type.IsAbstract && this.HasDefaultConstructor(type))
                {
                    object instance = Activator.CreateInstance(type);
                    IScript script = instance as IScript;

                    if (script != null)
                    {
                        instances.Add(script);
                    }
                }
            }

            return instances.ToArray();
        }
        /// <summary>
        /// Creates compiler parameters.
        /// </summary>
        private CompilerParameters CreateParameters()
        {
            CompilerParameters parameters = new CompilerParameters();
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
        public IScript[] Compile(params string[] sources)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            YieldEvaluator evaluator = new YieldEvaluator(this.Commands);

            string[] evaluatedSources = sources
                .Select(evaluator.Evaluate)
                .ToArray();
            
            CompilerResults result = codeProvider.CompileAssemblyFromSource(
                this.CreateParameters(), evaluatedSources);

            this.Succeed = result.Errors.Count == 0;
            if (!this.Succeed)
            {
                this.Errors.Clear();
                foreach (CompilerError error in result.Errors)
                {
                    this.Errors.Add(error);
                }

                return new IScript[] { };
            }

            return this.LoadInstances(result.CompiledAssembly);
        }
        #endregion
    }
}
