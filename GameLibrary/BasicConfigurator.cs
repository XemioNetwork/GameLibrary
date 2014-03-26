using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Config;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.FileSystem.Disk;
using Xemio.GameLibrary.Content.Formats;
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
            var config = Fluently.Configure()
                .ContentFormat(Format.Xml)
                .CreatePlayerInput()
                .DisableSplashScreen()
                .FileSystem(new DiskFileSystem());

            XGL.Run(config);
        }
        #endregion
    }
}
