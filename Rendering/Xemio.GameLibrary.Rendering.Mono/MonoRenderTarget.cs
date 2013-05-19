using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Mono
{
    using Drawing = System.Drawing;
    using Drawing2D = System.Drawing.Drawing2D;

    public class MonoRenderTarget : MonoTexture, IRenderTarget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoRenderTarget"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public MonoRenderTarget(int width, int height) : this(new Bitmap(width, height))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoRenderTarget"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public MonoRenderTarget(Bitmap bitmap) : base(bitmap)
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
        public Graphics Graphics { get; private set; }
        #endregion
    }
}
