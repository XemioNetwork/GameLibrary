using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Mono
{
    public class MonoGraphicsInitializer : IGraphicsInitializer
    {
        #region IGraphicsInitializer Member
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice)
        {
            return new MonoGraphicsProvider(graphicsDevice);
        }
        #endregion
    }
}
