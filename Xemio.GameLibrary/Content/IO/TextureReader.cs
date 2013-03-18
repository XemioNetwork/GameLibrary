using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Content.IO
{
    public class TextureReader : ContentReader<ITexture>
    {
        #region Properties
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public ITextureFactory Factory
        {
            get { return XGL.GetComponent<ITextureFactory>(); }
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
