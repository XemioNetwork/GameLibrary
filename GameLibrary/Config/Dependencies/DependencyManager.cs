using System;
using System.Collections.Generic;
using Xemio.GameLibrary.Config.Installation;

namespace Xemio.GameLibrary.Config.Dependencies
{
    public class DependencyManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyManager" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public DependencyManager(Configuration configuration)
        {
            this._configuration = configuration;
            this._dependencies = new List<Dependency>();
        }
        #endregion

        #region Fields
        private readonly Configuration _configuration;
        private readonly List<Dependency> _dependencies; 
        #endregion

        #region Methods
        /// <summary>
        /// Ensures the specified installer to be existent inside the configuation or throws a <see cref="DependencyException"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Dependency<T>() where T : class, IInstaller
        {
            this.Dependency<T>(() =>
            {
                //Throw a dependency exception, if the component
                //does not exist.
                throw new DependencyException(typeof(T));
            });
        }
        /// <summary>
        /// Ensures the specified installer to get installed if it is not provided by the configuration.
        /// </summary>
        /// <typeparam name="T">The installer type.</typeparam>
        /// <param name="defaultInstaller">The default installer.</param>
        public void Dependency<T>(Func<T> defaultInstaller) where T : class, IInstaller
        {
            this._dependencies.Add(new Dependency(this._configuration.Contains<T>, defaultInstaller));
        }
        /// <summary>
        /// Creates the required installers added by using the Dependency method.
        /// </summary>
        public void ResolveDependencies()
        {
            foreach (Dependency dependency in this._dependencies)
            {
                if (!dependency.Exists())
                {
                    this._configuration.Install(new PreInstaller(dependency.CreateInstaller()));
                }
            }
        }
        #endregion
    }
}
