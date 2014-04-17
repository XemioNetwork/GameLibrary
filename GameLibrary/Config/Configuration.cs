using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Config.Installation;

namespace Xemio.GameLibrary.Config
{
    public class Configuration : ListCatalog<IInstaller>
    {
        #region Properties
        /// <summary>
        /// Gets the installers.
        /// </summary>
        internal IEnumerable<IInstaller> Installers
        {
            get { return this.Items; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Injects the specified installer.
        /// </summary>
        /// <typeparam name="T">The installer type.</typeparam>
        /// <param name="installer">The installer.</param>
        public void Override<T>(T installer) where T : IInstaller
        {
            this.Remove<T>();
            this.Install(installer);
        }
        /// <summary>
        /// Installs the specified installer, if it is not contained inside the current configuration.
        /// </summary>
        /// <typeparam name="T">The installer type.</typeparam>
        public T GetOrInstall<T>() where T : IInstaller, new()
        {
            if (!this.Contains<T>())
            {
                this.Install(new T());
            }

            return this.Get<T>();
        }
        /// <summary>
        /// Determines whether the specified installer is contained inside this configuration.
        /// </summary>
        /// <typeparam name="T">The installer type.</typeparam>
        public bool Contains<T>() where T : IInstaller
        {
            return this.Items.Any(installer => installer.Unwrap() is T);
        }
        #endregion
    }
}
