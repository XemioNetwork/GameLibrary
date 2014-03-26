﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Config.Installers
{
    public class PluginInstaller : AbstractInstaller
    {
        #region Overrides of AbstractInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            catalog.Install(new LibraryLoader());
            catalog.Install(new ImplementationManager());
        }
        #endregion
    }
}
