using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Plugins.Contexts
{
    public class MergedAssemblyContext : IAssemblyContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MergedAssemblyContext"/> class.
        /// </summary>
        /// <param name="a">The first assembly context.</param>
        /// <param name="b">The second assembly context.</param>
        public MergedAssemblyContext(IAssemblyContext a, IAssemblyContext b)
        {
            var assemblies = new List<Assembly>();
            assemblies.AddRange(a.Assemblies);
            assemblies.AddRange(b.Assemblies);

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
