using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaRenderFactory : IRenderFactory
    {
        #region Implementation of IRenderFactory
        /// <summary>
        /// Creates a new render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IRenderTarget CreateTarget(int width, int height)
        {
            return new XnaRenderTarget(width, height);
        }
        #endregion
    }
}
