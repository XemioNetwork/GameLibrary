using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Plugins
{
    public class LibraryLoader : IConstructable
    {
        #region Methods
        /// <summary>
        /// Loads the libraries.
        /// </summary>
        /// <param name="context">The context.</param>
        public void LoadLibraries(IAssemblyContext context)
        {
            var initializers = from assembly in context.Assemblies
                               from type in assembly.GetTypes()
                               where
                                   !type.IsAbstract && !type.IsInterface && !type.IsGenericType &&
                                   typeof(ILibraryInitializer).IsAssignableFrom(type)
                               select Activator.CreateInstance(type) as ILibraryInitializer;

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            this.LoadLibraries(ContextFactory.CreateFileAssemblyContext("."));
        }
        #endregion
    }
}
