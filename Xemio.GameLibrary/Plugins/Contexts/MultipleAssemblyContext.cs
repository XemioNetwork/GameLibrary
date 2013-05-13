using System.Collections.Generic;
using System.Reflection;

namespace Xemio.GameLibrary.Plugins.Contexts
{
    internal class MultipleAssemblyContext : IAssemblyContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleAssemblyContext"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public MultipleAssemblyContext(IEnumerable<Assembly> assemblies)
        {
            this.Assemblies = assemblies;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleAssemblyContext"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public MultipleAssemblyContext(params Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
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
