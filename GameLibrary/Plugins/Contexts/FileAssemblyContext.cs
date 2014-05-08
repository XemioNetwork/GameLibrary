using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Logging;

namespace Xemio.GameLibrary.Plugins.Contexts
{
    internal class FileAssemblyContext : IAssemblyContext
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly HashSet<Assembly> _assemblies;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAssemblyContext"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public FileAssemblyContext(string directory)
        {
            this._assemblies = new HashSet<Assembly>();
            this.LoadAssemblies(directory);
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the assemblies out of the specified path.
        /// </summary>
        /// <param name="directory">The directory.</param>
        private void LoadAssemblies(string directory)
        {
            foreach (string fileName in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(fileName) != ".dll" && Path.GetExtension(fileName) != ".exe")
                    continue;

                try
                {
                    this._assemblies.Add(Assembly.LoadFrom(fileName));
                }
                catch (Exception exception)
                {
                    logger.ErrorException("Unexpected error occured: ", exception);
                }
            }

            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                this.LoadAssemblies(subDirectory);
            }
        }
        #endregion

        #region Implementation of IAssemblyContext
        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        public IEnumerable<Assembly> Assemblies
        {
            get { return this._assemblies; }
        }
        #endregion
    }
}
