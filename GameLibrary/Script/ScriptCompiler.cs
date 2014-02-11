using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using NLog;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Plugins.Contexts;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Script;

namespace Xemio.GameLibrary.Script
{
    using CodeDom = System.CodeDom.Compiler;

    public class ScriptCompiler
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptCompiler"/> class.
        /// </summary>
        public ScriptCompiler()
        {
            this.Assemblies = new List<string>();
            this.GenerateInMemory = true;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the output assembly.
        /// </summary>
        public string OutputAssembly { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to generate the assembly in memory.
        /// </summary>
        public bool GenerateInMemory { get; set; }
        /// <summary>
        /// Gets the assembly references.
        /// </summary>
        public List<string> Assemblies { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates compiler parameters.
        /// </summary>
        private CodeDom.CompilerParameters CreateParameters()
        {
            var parameters = new CodeDom.CompilerParameters
            {
                OutputAssembly = this.OutputAssembly,
                GenerateInMemory = this.GenerateInMemory
            };

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Linq.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add(typeof(XGL).Assembly.Location);

            parameters.ReferencedAssemblies.AddRange(this.Assemblies.ToArray());

            return parameters;
        }
        /// <summary>
        /// Compiles the specified sources.
        /// </summary>
        /// <param name="sources">The sources.</param>
        public CompilerResult Compile(params string[] sources)
        {
            var codeProvider = new CSharpCodeProvider();
            
            CodeDom.CompilerResults result = codeProvider.CompileAssemblyFromSource(
                this.CreateParameters(), sources);
            
            if (result.Errors.Count > 0)
            {
                var errors = new List<CompilerError>();
                errors.AddRange(from CodeDom.CompilerError error in result.Errors select new CompilerError(error.Line, error.ErrorText));

                var sourceCode = new StringBuilder();
                foreach (string source in sources)
                {
                    sourceCode.AppendLine(source);
                }

                logger.Error("Error while compiling script: {0}", sourceCode);
                foreach (CompilerError error in errors)
                {
                    logger.Error("Line {0}: {1}", error.Line, error.Message);
                }

                return new CompilerResult(errors);
            }

            return new CompilerResult(result.CompiledAssembly);
        }
        #endregion
    }
}
