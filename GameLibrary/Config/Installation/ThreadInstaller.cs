using Xemio.GameLibrary.Common.Threads;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Config.Installation
{
    public class ThreadInstaller : AbstractInstaller
    {
        #region Overrides of AbstractInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            catalog.Install(new ThreadInvoker());
        }
        #endregion
    }
}
