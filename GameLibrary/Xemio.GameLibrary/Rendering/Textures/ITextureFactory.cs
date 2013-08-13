using System.Drawing;
using System.IO;
using System.Resources;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering.Textures
{
    [AbstractComponent]
    public interface ITextureFactory : IComponent
    {
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        ITexture CreateTexture(Bitmap bitmap);
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
        /// <summary>
        /// Creates a render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        IRenderTarget CreateRenderTarget(int width, int height);
    }
}
