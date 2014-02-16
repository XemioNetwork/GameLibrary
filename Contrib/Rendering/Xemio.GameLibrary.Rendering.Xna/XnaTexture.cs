using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaTexture : ITexture
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaTexture"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public XnaTexture(Texture2D texture)
        {
            this.Texture = texture;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the xna texture.
        /// </summary>
        public Texture2D Texture { get; private set; }
        #endregion
        
        #region Implementation of ITexture
        /// <summary>
        /// Gets the texture data.
        /// </summary>
        public byte[] GetData()
        {
            var stream = new MemoryStream();
            this.Texture.SaveAsPng(stream, this.Width, this.Height);

            return stream.ToArray();
        }
        /// <summary>
        /// Sets the texture data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetData(byte[] data)
        {
            this.Texture = Texture2D.FromStream(XnaHelper.GraphicsDevice, new MemoryStream(data));
        }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return this.Texture.Width; }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return this.Texture.Height; }
        }
        #endregion
    }
}
