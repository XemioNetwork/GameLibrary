using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary
{
    public class BasicConfigurator
    {
        #region Static Methods
        /// <summary>
        /// Configures the XGL core components to provide access to core functionality.
        /// </summary>
        public static void Configure()
        {
            var config = XGL.Configure()
                .DisableGameLoop()
                .DisableInput()
                .DisableSplashScreen()
                .FileSystem<DiskFileSystem>()
                .EnableCoreComponents()
                .BuildConfiguration();

            XGL.Run(config);
        }
        #endregion
    }
}
