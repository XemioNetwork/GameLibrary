using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using SdlDotNet.Graphics;

namespace Xemio.GameLibrary.Rendering.SDL
{
    public class SDLRenderTarget : SDLTexture, IRenderTarget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SDLRenderTarget"/> class.
        /// </summary>
        /// <param name="surface"></param>
        public SDLRenderTarget(Surface surface) : base(surface)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SDLRenderTarget"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public SDLRenderTarget(int width, int height) 
            : base(new Surface(width, height, 32, 255, 255, 255, 255))
        {
        }
        #endregion
    }
}
