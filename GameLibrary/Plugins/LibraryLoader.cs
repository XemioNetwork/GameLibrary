using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Logging;

namespace Xemio.GameLibrary.Plugins
{
    public class LibraryLoader : IConstructable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        /// <summary>
        /// Loads the libraries.
        /// </summary>
        /// <param name="context">The context.</param>
        public void LoadLibraries(IAssemblyContext context)
        {
            var initializers = this.GetInitializer(context);

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the initializers from the specified assembly context.
        /// </summary>
        /// <param name="context">The context.</param>
        private IEnumerable<ILibraryInitializer> GetInitializer(IAssemblyContext context)
        {
            IList<Assembly> failedAssemblies = new List<Assembly>();

            foreach (Assembly assembly in context.Assemblies)
            {
                IEnumerable<ILibraryInitializer> initializers;
                if (!this.TryGetInitializerFromAssembly(assembly, out initializers))
                {
                    logger.Debug("Failed to load assembly {0} for the first time.", assembly.FullName);
                    failedAssemblies.Add(assembly);
                }

                foreach (ILibraryInitializer initializer in initializers)
                {
                    logger.Trace("Found initializer {0}.", initializer.GetType().Name);
                    yield return initializer;
                }
            }

            logger.Debug("Trying to reload failed assemblies.");

            //Retry all failed assemblies
            foreach (Assembly assembly in failedAssemblies)
            {
                IEnumerable<ILibraryInitializer> initializers;

                //Ignore if they fail now
                if (!this.TryGetInitializerFromAssembly(assembly, out initializers))
                {
                    logger.Debug("Loading {0} failed. Ignoring library initializers now.", assembly.FullName);
                }

                foreach (ILibraryInitializer initializer in initializers)
                {
                    yield return initializer;
                }
            }
        }
        /// <summary>
        /// Tries to get the initializer from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="result">The result.</param>
        private bool TryGetInitializerFromAssembly(Assembly assembly, out IEnumerable<ILibraryInitializer> result)
        {
            try
            {
                result = from type in assembly.GetTypes()
                         where
                             !type.IsAbstract && !type.IsInterface && !type.IsGenericType &&
                             typeof(ILibraryInitializer).IsAssignableFrom(type)
                         select Activator.CreateInstance(type) as ILibraryInitializer;

                return true;
            }
            catch
            {
                result = new List<ILibraryInitializer>();

                return false;
            }
        }
        #endregion Private Methods

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
