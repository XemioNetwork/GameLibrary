using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Config.Installers
{
    internal class PreInstaller : AbstractInstaller, INestedInstaller
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PreInstaller"/> class.
        /// </summary>
        /// <param name="installer">The installer.</param>
        public PreInstaller(IInstaller installer)
        {
            this.Child = installer;
        }
        #endregion
        
        #region Overrides of AbstractInstaller
        /// <summary>
        /// Handles pre installation steps.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void PreInstall(Configuration configuration, IComponentCatalog catalog)
        {
            this.Child.PreInstall(configuration, catalog);
            this.Child.Install(configuration, catalog);
        }
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
        }
        /// <summary>
        /// Handles post installation steps.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void PostInstall(Configuration configuration, IComponentCatalog catalog)
        {
            this.Child.PostInstall(configuration, catalog);
        }
        #endregion

        #region Implementation of INestedInstaller
        /// <summary>
        /// Gets the child.
        /// </summary>
        public IInstaller Child { get; private set; }
        #endregion
    }
}
