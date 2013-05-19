using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.GDIPlus
{
    public class GDIRenderTarget : GDITexture, IRenderTarget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIRenderTarget"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public GDIRenderTarget(int width, int height) : base(new Bitmap(width, height))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIRenderTarget"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public GDIRenderTarget(Bitmap bitmap) : base(bitmap)
        {
        }
        #endregion

    }
}
