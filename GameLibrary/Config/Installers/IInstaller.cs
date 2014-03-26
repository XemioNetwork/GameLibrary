using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Dependencies;
using Xemio.GameLibrary.Config.Validation;

namespace Xemio.GameLibrary.Config.Installers
{
    public interface IInstaller
    {
        /// <summary>
        /// Sets up the dependencies to other installers.
        /// </summary>
        /// <param name="manager">The manager.</param>
        void SetupDependencies(DependencyManager manager);
        /// <summary>
        /// Sets up the conditions for your installation to complete.
        /// </summary>
        /// <param name="manager">The manager.</param>
        void SetupConditions(ValidationManager manager);
        /// <summary>
        /// Handles pre installation steps.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        void PreInstall(Configuration configuration, IComponentCatalog catalog);
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        void Install(Configuration configuration, IComponentCatalog catalog);
        /// <summary>
        /// Handles post installation steps.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        void PostInstall(Configuration configuration, IComponentCatalog catalog);
    }
}
