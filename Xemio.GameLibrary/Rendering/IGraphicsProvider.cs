using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering
{
    public interface IGraphicsProvider : IComponent
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        GraphicsDevice GraphicsDevice { get; }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        ITextureFactory TextureFactory { get; }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        IRenderManager RenderManager { get; }
    }
}
