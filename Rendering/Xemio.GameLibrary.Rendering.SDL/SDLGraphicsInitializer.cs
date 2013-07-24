using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.SDL
{
    public class SDLGraphicsInitializer : IGraphicsInitializer
    {
        #region Implementation of IGraphicsInitializer
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        public bool IsAvailable()
        {
            return true;
        }
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice)
        {
            return new SDLGraphicsProvider(graphicsDevice);
        }
        #endregion
    }
}
