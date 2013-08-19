using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering
{
    public interface IRenderFactory
    {
        /// <summary>
        /// Creates a new render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        IRenderTarget CreateTarget(int width, int height);
    }
}
