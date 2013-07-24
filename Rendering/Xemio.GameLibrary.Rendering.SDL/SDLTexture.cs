using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using SdlDotNet.Graphics;

namespace Xemio.GameLibrary.Rendering.SDL
{
    public class SDLTexture : ITexture
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SDLTexture"/> class.
        /// </summary>
        public SDLTexture(Surface surface)
        {
            this.Surface = surface;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the surface.
        /// </summary>
        public Surface Surface { get; private set; }
        #endregion
        
        #region Implementation of ITexture
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return this.Surface.Width; }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return this.Surface.Height; }
        }
        /// <summary>
        /// Gets the texture data.
        /// </summary>
        public byte[] GetData()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sets the texture data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetData(byte[] data)
        {
            this.Surface = new Surface(data);
        }
        #endregion
    }
}
