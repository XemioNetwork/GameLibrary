using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering.GDIPlus
{
    using Drawing = System.Drawing;
    using Drawing2D = System.Drawing.Drawing2D;

    using Rectangle = Xemio.GameLibrary.Math.Rectangle;
    using Color = System.Drawing.Color;

    public class GDIRenderManager : IRenderManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIRenderManager"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public GDIRenderManager(GDIGraphicsProvider graphicsProvider)
        {
            this._offset = Vector2.Zero;
            this._graphicsProvider = graphicsProvider;
        }
        #endregion

        #region Fields
        private Bitmap _buffer;
        private Graphics _bufferGraphics;

        private Vector2 _offset;

        private GDIGraphicsProvider _graphicsProvider;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the buffer.
        /// </summary>
        private bool InitializeBuffer()
        {
            Control target = Control.FromHandle(this._graphicsProvider.Handle);

            if (target != null)
            {
                int width = target.ClientSize.Width;
                int height = target.ClientSize.Height;

                if (this._buffer == null ||
                    this._buffer.Width != width || this._buffer.Height != height)
                {
                    this._buffer = new Bitmap(
                        width, height,
                        PixelFormat.Format32bppPArgb);

                    this._bufferGraphics = Graphics.FromImage(this._buffer);
                    this._bufferGraphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor;
                    this._bufferGraphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed;
                    this._bufferGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed;
                    this._bufferGraphics.CompositingQuality = Drawing2D.CompositingQuality.AssumeLinear;

                    this._bufferGraphics.Clear(Drawing.Color.Black);
                }

                return true;
            }

            return false;
        }
        #endregion

        #region IRenderProvider Member
        /// <summary>
        /// Gets the graphics provider.
        /// </summary>
        public IGraphicsProvider GraphicsProvider
        {
            get { return this._graphicsProvider; }
        }
        /// <summary>
        /// Offsets the screen.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public void Offset(Vector2 offset)
        {
            this._offset = offset;
        }
        /// <summary>
        /// Sets the rotation to the specified angle in radians.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        public void Rotate(float rotation)
        {
        }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Xemio.GameLibrary.Rendering.Color color)
        {
            Color drawingColor = Color.FromArgb(
                color.A,
                color.R,
                color.G, 
                color.B);

            if (this.InitializeBuffer())
            {
                this._bufferGraphics.Clear(drawingColor);
            }
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public void Render(ITexture texture, Vector2 position)
        {
            this.Render(texture, new Rectangle(position.X, position.Y, texture.Width, texture.Height));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public void Render(ITexture texture, Rectangle destination)
        {
            this.Render(texture, destination, Rectangle.Empty);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
            if (this.InitializeBuffer())
            {
                GDITexture gdiTexture = texture as GDITexture;
                Control target = Control.FromHandle(this._graphicsProvider.Handle);

                int screenWidth = target.Width;
                int screenHeight = target.Height;

                if (gdiTexture == null)
                {
                    throw new InvalidOperationException("The rendered texture has to be an instance of the GDITexture class.");
                }

                destination += this._offset;

                this._bufferGraphics.DrawImage(
                    gdiTexture.Bitmap,
                    (int)destination.X,
                    (int)destination.Y,
                    (int)destination.Width,
                    (int)destination.Height);
            }
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            Control target = Control.FromHandle(this._graphicsProvider.Handle);

            if (target != null)
            {
                int screenWidth = target.Width;
                int screenHeight = target.Height;

                IntPtr windowHandle = this._graphicsProvider.Handle;
                Control surface = Control.FromHandle(windowHandle);

                Drawing.Graphics graphics = surface.CreateGraphics();

                IntPtr hDC = graphics.GetHdc();
                IntPtr hMemDC = GDIHelper.CreateCompatibleDC(hDC);
                IntPtr bufferDC = this._buffer.GetHbitmap();

                GDIHelper.SelectObject(hMemDC, bufferDC);

                GDIHelper.StretchBlt
                (
                    hDC, 0, 0,
                    surface.Width,
                    surface.Height,
                    hMemDC,
                    0, 0, screenWidth, screenHeight,
                    GDIRasterOperations.SRCCOPY
                );

                GDIHelper.DeleteObject(bufferDC);
                GDIHelper.DeleteObject(hMemDC);

                graphics.ReleaseHdc(hDC);
            }

            this._offset = Vector2.Zero;
            this._bufferGraphics.Clear(Drawing.Color.Black);
        }
        #endregion
    }
}
