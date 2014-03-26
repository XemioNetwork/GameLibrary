using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Dependencies;
using Xemio.GameLibrary.Config.Validation;

namespace Xemio.GameLibrary.Config.Installers
{
    public abstract class AbstractInstaller : IInstaller
    {
        #region Implementation of IInstaller
        /// <summary>
        /// Sets up the dependencies to other installers.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public virtual void SetupDependencies(DependencyManager manager)
        {
        }
        /// <summary>
        /// Sets up the conditions for your installation to complete.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public virtual void SetupConditions(ValidationManager manager)
        {
        }
        /// <summary>
        /// Handles pre installation steps.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public virtual void PreInstall(Configuration configuration, IComponentCatalog catalog)
        {
        }
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public virtual void Install(Configuration configuration, IComponentCatalog catalog)
        {
        }
        /// <summary>
        /// Handles post installation steps.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public virtual void PostInstall(Configuration configuration, IComponentCatalog catalog)
        {
        }
        #endregion
    }
}
