using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Config.Installers
{
    public class EventInstaller : AbstractInstaller
    {
        #region Implementation of IInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            var eventManager = new EventManager();
            eventManager.LoadEventsFromAssemblyOf<XGL>();

            catalog.Install(eventManager);
        }
        #endregion
    }
}
