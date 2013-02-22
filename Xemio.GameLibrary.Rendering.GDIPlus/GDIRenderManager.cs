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
        /// <param name="graphicsDevice">The graphics device.</param>
        public GDIRenderManager(GraphicsDevice graphicsDevice)
        {
            this._offset = Vector2.Zero;
            this.GraphicsDevice = graphicsDevice;
        }
        #endregion

        #region Fields
        private Bitmap _buffer;
        private Graphics _bufferGraphics;

        private Vector2 _offset;

        private Xemio.GameLibrary.Rendering.Color _color;
        private ImageAttributes _attributes;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the buffer.
        /// </summary>
        private bool InitializeBuffer()
        {
            Control target = Control.FromHandle(this.GraphicsDevice.Handle);

            if (target != null)
            {
                int width = target.ClientSize.Width;
                int height = target.ClientSize.Height;

                PixelFormat pixelFormat = PixelFormat.Format32bppPArgb;

                if (this._buffer == null ||
                    this._buffer.Width != width || this._buffer.Height != height)
                {
                    this._buffer = new Bitmap(width, height, pixelFormat);

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
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
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
        /// Sets the opacity.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Tint(Xemio.GameLibrary.Rendering.Color color)
        {
            this._color = color;

            float a = color.A / 255.0f;
            float r = color.R / 255.0f;
            float g = color.G / 255.0f;
            float b = color.B / 255.0f;

            ColorMatrix matrix = new ColorMatrix(new float[][]
            {
                new float[] { r, 0, 0, 0, 0 },
                new float[] { 0, g, 0, 0, 0 },
                new float[] { 0, 0, b, 0, 0 },
                new float[] { 0, 0, 0, a, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            });

            this._attributes = new ImageAttributes();
            this._attributes.SetColorMatrix(matrix, ColorMatrixFlag.SkipGrays, ColorAdjustType.Bitmap);
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
                Control target = Control.FromHandle(this.GraphicsDevice.Handle);

                int screenWidth = target.Width;
                int screenHeight = target.Height;

                if (gdiTexture == null)
                {
                    throw new InvalidOperationException("The rendered texture has to be an instance of the GDITexture class.");
                }

                destination += this._offset;

                if (this._color != Xemio.GameLibrary.Rendering.Color.White)
                {
                    this._bufferGraphics.DrawImage(
                        gdiTexture.Bitmap,
                        new Drawing.Rectangle(
                            (int)destination.X,
                            (int)destination.Y,
                            (int)destination.Width,
                            (int)destination.Height),
                        0, 0, destination.Width, destination.Height, GraphicsUnit.Pixel,
                        this._attributes);
                }
                else
                {
                    this._bufferGraphics.DrawImage(
                        gdiTexture.Bitmap,
                        (int)destination.X,
                        (int)destination.Y,
                        (int)destination.Width,
                        (int)destination.Height);
                }

                this._bufferGraphics.ResetTransform();
            }
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            Control surface = Control.FromHandle(this.GraphicsDevice.Handle);

            if (surface != null)
            {
                int screenWidth = surface.Width;
                int screenHeight = surface.Height;
                
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
