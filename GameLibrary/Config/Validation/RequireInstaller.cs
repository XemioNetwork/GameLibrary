using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Installation;

namespace Xemio.GameLibrary.Config.Validation
{
    public class RequireInstaller<T> : IValidator where T : class, IInstaller
    {
        #region Implementation of ICondition
        /// <summary>
        /// Gets the default message.
        /// </summary>
        public string DefaultMessage
        {
            get { return "Installer " + typeof (T) + " is required by an installer. Make sure you installed the missing installer."; }
        }
        /// <summary>
        /// Gets a value indicating whether the condition is fulfilled.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public bool Validate(Configuration configuration, IComponentCatalog catalog)
        {
            return configuration.Contains<T>();
        }
        #endregion
    }
}
