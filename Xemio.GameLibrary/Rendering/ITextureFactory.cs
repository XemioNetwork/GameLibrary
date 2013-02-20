using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering
{
    public interface ITextureFactory : IComponent
    {
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="fileName">The texture filename.</param>
        ITexture CreateTexture(string fileName);
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="resourceManager">The resource manager.</param>
        ITexture CreateTexture(string name, ResourceManager resourceManager);
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="stream">The stream.</param>
        ITexture CreateTexture(Stream stream);
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="data">The binary texture data.</param>
        ITexture CreateTexture(byte[] data);
    }
}
