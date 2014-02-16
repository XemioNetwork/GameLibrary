using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaRenderTarget : XnaTexture, IRenderTarget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaRenderTarget"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public XnaRenderTarget(int width, int height)
            : this(new RenderTarget2D(XnaHelper.GraphicsDevice, width, height))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaRenderTarget"/> class.
        /// </summary>
        /// <param name="renderTarget">The render target.</param>
        public XnaRenderTarget(RenderTarget2D renderTarget) : base(renderTarget)
        {
            this.RenderTarget = renderTarget;

            renderTarget.GraphicsDevice.SetRenderTarget(renderTarget);
            renderTarget.GraphicsDevice.Clear(XnaHelper.Convert(Color.Transparent));

            renderTarget.GraphicsDevice.SetRenderTarget(null);
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the render target.
        /// </summary>
        public RenderTarget2D RenderTarget { get; private set; }
        #endregion
    }
}
