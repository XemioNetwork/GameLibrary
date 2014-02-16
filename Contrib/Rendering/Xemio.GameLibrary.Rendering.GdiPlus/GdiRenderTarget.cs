using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    using Drawing = System.Drawing;
    using Drawing2D = System.Drawing.Drawing2D;

    public class GdiRenderTarget : GdiTexture, IRenderTarget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiRenderTarget"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public GdiRenderTarget(int width, int height)
            : this(new Bitmap(width, height, PixelFormat.Format32bppPArgb))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiRenderTarget"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public GdiRenderTarget(Bitmap bitmap) : base(bitmap)
        {
            this.Graphics = Graphics.FromImage(bitmap);

            this.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor;
            this.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality;
            this.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed;
            this.Graphics.CompositingQuality = Drawing2D.CompositingQuality.AssumeLinear;

            this.Graphics.Clear(Drawing.Color.Transparent);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        internal Graphics Graphics { get; private set; }
        #endregion
    }
}
