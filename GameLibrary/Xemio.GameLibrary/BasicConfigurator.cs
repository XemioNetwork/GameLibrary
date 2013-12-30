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
            XGL.Components.Add(new EventManager());
            XGL.Components.Add(new ImplementationManager());
            XGL.Components.Add(new ThreadInvoker());
            XGL.Components.Add(new SerializationManager());
            XGL.Components.Add(new ContentManager());
            XGL.Components.Add(new DiskFileSystem());
            XGL.Components.Construct();

            XGL.State = XGLState.Initialized;
        }
        #endregion
    }
}
