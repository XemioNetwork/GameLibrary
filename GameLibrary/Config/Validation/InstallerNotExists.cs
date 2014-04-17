using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Installation;

namespace Xemio.GameLibrary.Config.Validation
{
    public class InstallerNotExists<T> : IValidator where T : IInstaller
    {
        #region Implementation of IValidator
        /// <summary>
        /// Gets the default message.
        /// </summary>
        public string DefaultMessage
        {
            get { return "Installer " + typeof(T) + " is required to be not existent inside the component registry."; }
        }
        /// <summary>
        /// Gets a value indicating whether the condition is fulfilled.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public bool Validate(Configuration configuration, IComponentCatalog catalog)
        {
            return !configuration.Contains<T>();
        }
        #endregion
    }
}
