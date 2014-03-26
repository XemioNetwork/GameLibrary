using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Config.Installers
{
    public class SerializationInstaller : AbstractInstaller
    {
        #region Implementation of IInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            catalog.Install(new SerializationManager());
        }
        #endregion
    }
}
