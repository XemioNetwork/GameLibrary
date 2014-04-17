using System.Collections.Generic;
using NLog;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game.Scenes;

namespace Xemio.GameLibrary.Config.Installation
{
    public class SceneInstaller : AbstractInstaller
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneInstaller"/> class.
        /// </summary>
        public SceneInstaller()
        {
            this.Scenes = new List<Scene>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneInstaller"/> class.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public SceneInstaller(IEnumerable<Scene> scenes) : this(scenes, true)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneInstaller"/> class.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        /// <param name="splashScreenEnabled">if set to <c>true</c> the scene manager will display a splash screen at start.</param>
        public SceneInstaller(IEnumerable<Scene> scenes, bool splashScreenEnabled)
        {
            this.Scenes = new List<Scene>(scenes);
            this.SplashScreenEnabled = splashScreenEnabled;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the scenes.
        /// </summary>
        public List<Scene> Scenes { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the scene manager will display a splash screen at start.
        /// </summary>
        public bool SplashScreenEnabled { get; set; }
        #endregion

        #region Overrides of AbstractInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            var sceneManager = new SceneManager();

            logger.Info("Initializing scenes. Count: {0}", this.Scenes.Count);

            if (this.SplashScreenEnabled)
            {
                logger.Info("Splash screen is enabled. Creating splash screen.");

                var splashScreen = new SplashScreen(this.Scenes);
                sceneManager.Add(splashScreen);
            }
            else
            {
                sceneManager.Add(this.Scenes);
            }

            catalog.Install(sceneManager);
        }
        #endregion
    }
}
