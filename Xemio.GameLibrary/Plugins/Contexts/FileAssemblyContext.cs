﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Xemio.GameLibrary.Plugins.Contexts
{
    internal class FileAssemblyContext : IAssemblyContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAssemblyContext"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public FileAssemblyContext(string directory)
        {
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
            List<Assembly> assemblies = new List<Assembly>();
            foreach (string fileName in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(fileName) == ".dll" ||
                    Path.GetExtension(fileName) == ".exe")
                {
                    assemblies.Add(Assembly.LoadFile(fileName));
                }
            }

            this.Assemblies = this.Assemblies.Union(assemblies);
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
        public IEnumerable<Assembly> Assemblies { get; private set; }
        #endregion
    }
}