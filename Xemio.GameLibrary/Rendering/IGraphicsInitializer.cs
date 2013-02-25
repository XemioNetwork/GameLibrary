using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering
{
    public interface IGraphicsInitializer : IComponent
    {
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice);
    }
}
