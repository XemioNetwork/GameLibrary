using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Plugins.Contexts;

namespace Xemio.GameLibrary.Plugins
{
    public static class ContextFactory
    {
        #region Methods
        /// <summary>
        /// Creates a single assembly context.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static IAssemblyContext CreateSingleAssemblyContext(Assembly assembly)
        {
            return new SingleAssemblyContext(assembly);
        }
        /// <summary>
        /// Creates a multiple assembly context.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public static IAssemblyContext CreateMultipleAssemblyContext(params Assembly[] assemblies)
        {
            return new MultipleAssemblyContext(assemblies);
        }
        /// <summary>
        /// Creates a multiple assembly context.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public static IAssemblyContext CreateMultipleAssemblyContext(IEnumerable<Assembly> assemblies)
        {
            return new MultipleAssemblyContext(assemblies);
        }
        /// <summary>
        /// Creates a file assembly context.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public static IAssemblyContext CreateFileAssemblyContext(string directory)
        {
            return new FileAssemblyContext(directory);
        }
        #endregion
    }
}
