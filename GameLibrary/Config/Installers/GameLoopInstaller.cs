using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Dependencies;
using Xemio.GameLibrary.Game;

namespace Xemio.GameLibrary.Config.Installers
{
    public class GameLoopInstaller : AbstractInstaller
    {
        #region Properties
        /// <summary>
        /// Gets or sets the target frame time.
        /// </summary>
        public float TargetFrameTime { get; set; }
        /// <summary>
        /// Gets or sets the lag compensation.
        /// </summary>
        public LagCompensation LagCompensation { get; set; }
        #endregion

        #region Implementation of IInstaller
        /// <summary>
        /// Sets up the dependencies to other installers.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public override void SetupDependencies(DependencyManager manager)
        {
            manager.Dependency(() => new EventInstaller());
            manager.Dependency(() => new ThreadInstaller());
        }
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            IGameLoop gameLoop = new GameLoop();
            gameLoop.TargetFrameTime = this.TargetFrameTime;
            gameLoop.LagCompensation = this.LagCompensation;

            catalog.Install(gameLoop);
        }
        /// <summary>
        /// Handles post installation steps.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void PostInstall(Configuration configuration, IComponentCatalog catalog)
        {
            var gameLoop = catalog.Get<IGameLoop>();
            gameLoop.Run();
        }
        #endregion
    }
}
