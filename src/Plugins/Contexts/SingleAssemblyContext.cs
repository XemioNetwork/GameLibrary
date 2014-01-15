using System.Collections.Generic;
using System.Reflection;

namespace Xemio.GameLibrary.Plugins.Contexts
{
    internal class SingleAssemblyContext : IAssemblyContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleAssemblyContext"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public SingleAssemblyContext(Assembly assembly)
        {
            this.Assemblies = new[] { assembly };
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
