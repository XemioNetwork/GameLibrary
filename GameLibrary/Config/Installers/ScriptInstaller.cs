using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Script;

namespace Xemio.GameLibrary.Config.Installers
{
    public class ScriptInstaller : AbstractInstaller
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInstaller"/> class.
        /// </summary>
        public ScriptInstaller() : this(".")
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInstaller"/> class.
        /// </summary>
        /// <param name="rootDirectory">The root directory.</param>
        public ScriptInstaller(string rootDirectory)
        {
            this.RootDirectory = rootDirectory;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the root directory.
        /// </summary>
        public string RootDirectory { get; set; }
        #endregion

        #region Overrides of AbstractInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            catalog.Install(new ScriptExecutor());
            catalog.Install(new DynamicScriptLoader(this.RootDirectory));
        }
        #endregion
    }
}
