using System.IO;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Rendering.Textures
{
    public class TextureReader : ContentReader<ITexture>
    {
        #region Properties
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public ITextureFactory Factory
        {
            get { return XGL.Components.Get<ITextureFactory>(); }
        }
        #endregion
        
        #region ContentReader<ITexture> Member
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override ITexture Read(BinaryReader reader)
        {
            return this.Factory.CreateTexture(reader.BaseStream);
        }
        #endregion
    }
}
