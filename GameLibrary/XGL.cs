using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config;
using Xemio.GameLibrary.Config.Dependencies;
using Xemio.GameLibrary.Config.Installation;
using Xemio.GameLibrary.Config.Validation;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input.Adapters;
using Xemio.GameLibrary.Localization;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Rendering.Surfaces;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary
{
    public class XGL
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the <see cref="XGL"/> class.
        /// </summary>
        static XGL()
        {
            XGL.Components = new ComponentCatalog();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the component manager.
        /// </summary>
        public static IComponentCatalog Components { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Configuration"/> is initialized.
        /// </summary>
        public static EngineState State { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Starts the XGL with the specified configuration.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        public static void Run(FluentConfiguration fluent)
        {
            XGL.Run(fluent.GetConfiguration());
        }
        /// <summary>
        /// Starts the XGL with the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void Run(Configuration configuration)
        {
            if (XGL.State == EngineState.Initialized)
            {
                logger.Warn("The library has already been configured.");
                return;
            }

            XGL.State = EngineState.Initializing;

            var dependencyManager = new DependencyManager(configuration);
            foreach (IInstaller installer in configuration.Installers)
            {
                installer.SetupDependencies(dependencyManager);
            }

            dependencyManager.ResolveDependencies();

            var validationManager = new ValidationManager();
            foreach (IInstaller installer in configuration.Installers)
            {
                installer.SetupConditions(validationManager);
            }

            var scopes = new[] {ValidationScope.Pre, ValidationScope.Install, ValidationScope.Post};
            
            foreach (var scope in scopes)
            {
                if (scope == ValidationScope.Post)
                {
                    XGL.Components.Construct();
                }

                foreach (IInstaller installer in configuration.Installers)
                {
                    switch (scope)
                    {
                        case ValidationScope.Pre:
                            installer.PreInstall(configuration, XGL.Components);
                            break;
                        case ValidationScope.Install:
                            installer.Install(configuration, XGL.Components);
                            break;
                        case ValidationScope.Post:
                            installer.PostInstall(configuration, XGL.Components);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                
                validationManager.Validate(scope, configuration, XGL.Components);
            }

            XGL.State = EngineState.Initialized;
        }
        #endregion
    }
}
