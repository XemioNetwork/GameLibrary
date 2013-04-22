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
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Rendering.Mono
{
    using Drawing = System.Drawing;
    using Drawing2D = System.Drawing.Drawing2D;

    using Rectangle = Xemio.GameLibrary.Math.Rectangle;
    using Color = System.Drawing.Color;

    public class MonoRenderManager : IRenderManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIRenderManager"/> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public MonoRenderManager(GraphicsDevice graphicsDevice)
        {
            this.ScreenOffset = Vector2.Zero;

            this.GraphicsDevice = graphicsDevice;
            this.GraphicsDevice.ResolutionChanged += GraphicsDeviceResolutionChanged;

            this.InitializeBuffer(new DisplayMode(1, 1));
        }
        #endregion

        #region Fields
        private Bitmap _buffer;
        private bool _initializedPaint;

        private Xemio.GameLibrary.Rendering.Color _color;
        private ImageAttributes _attributes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the buffer graphics.
        /// </summary>
        public Graphics BufferGraphics { get; private set; }
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public Vector2 ScreenOffset { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the buffer.
        /// </summary>
        private void InitializeBuffer(DisplayMode mode)
        {
            Control target = Control.FromHandle(this.GraphicsDevice.Handle);

            if (target != null)
            {
                int width = mode.Width;
                int height = mode.Height;

                PixelFormat pixelFormat = PixelFormat.Format32bppPArgb;

                this._buffer = new Bitmap(width, height, pixelFormat);

                this.BufferGraphics = Graphics.FromImage(this._buffer);
                this.BufferGraphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor;
                this.BufferGraphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed;
                this.BufferGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed;
                this.BufferGraphics.CompositingQuality = Drawing2D.CompositingQuality.AssumeLinear;

                this.BufferGraphics.Clear(Drawing.Color.Black);
            }
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
        public void Translate(Vector2 offset)
        {
            this.ScreenOffset = offset;
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
        public void Tint(Rendering.Color color)
        {
            this._color = color;

            if (color == Rendering.Color.White)
            {
                if (this._attributes != null)
                {
                    this._attributes.ClearColorMatrix();
                }

                return;
            }

            float a = color.A / 255.0f;
            float r = color.R / 255.0f;
            float g = color.G / 255.0f;
            float b = color.B / 255.0f;

            ColorMatrix matrix = new ColorMatrix(new []
            {
                new[] { r, 0, 0, 0, 0 },
                new[] { 0, g, 0, 0, 0 },
                new[] { 0, 0, b, 0, 0 },
                new[] { 0, 0, 0, a, 0 },
                new[] { 0, 0, 0, 0, 1f }
            });

            this._attributes = new ImageAttributes();
            this._attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Rendering.Color color)
        {
            Color drawingColor = Color.FromArgb(
                color.A,
                color.R,
                color.G, 
                color.B);

            this.BufferGraphics.Clear(drawingColor);
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
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Vector2 position, Rendering.Color color)
        {
            this.Tint(color);
            this.Render(texture, position);

            this.Tint(Rendering.Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="color"></param>
        public void Render(ITexture texture, Rectangle destination, Rendering.Color color)
        {
            this.Tint(color);
            this.Render(texture, destination);

            this.Tint(Rendering.Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin, Rendering.Color color)
        {
            this.Tint(color);
            this.Render(texture, destination, origin);

            this.Tint(Rendering.Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
            MonoTexture monoTexture = texture as MonoTexture;
            Control surface = Control.FromHandle(this.GraphicsDevice.Handle);
            
            if (monoTexture == null)
            {
                throw new InvalidOperationException("The rendered texture has to be an instance of the MonoTexture class.");
            }

            if (this._color != Xemio.GameLibrary.Rendering.Color.White)
            {
                this.BufferGraphics.DrawImage(
                    monoTexture.Bitmap,
                    new Drawing.Rectangle(
                        (int)destination.X + (int)this.ScreenOffset.X,
                        (int)destination.Y + (int)this.ScreenOffset.Y,
                        (int)destination.Width,
                        (int)destination.Height),
                    0, 0, destination.Width, destination.Height, GraphicsUnit.Pixel,
                    this._attributes);
            }
            else
            {
                this.BufferGraphics.DrawImage(
                    monoTexture.Bitmap,
                    (int)destination.X + (int)this.ScreenOffset.X,
                    (int)destination.Y + (int)this.ScreenOffset.Y,
                    (int)destination.Width,
                    (int)destination.Height);
            }

            this.BufferGraphics.ResetTransform();
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            Control surface = Control.FromHandle(this.GraphicsDevice.Handle);

            if (surface != null)
            {
                if (!this._initializedPaint)
                {
                    surface.Paint += new PaintEventHandler(surface_Paint);
                    this._initializedPaint = true;
                }

                ThreadInvoker.Invoke(() => surface.Invalidate());
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the ResolutionChanged event of the GraphicsDevice.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GraphicsDeviceResolutionChanged(object sender, EventArgs e)
        {
            this.InitializeBuffer(this.GraphicsDevice.DisplayMode);
        }
        /// <summary>
        /// Handles the Paint event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void surface_Paint(object sender, PaintEventArgs e)
        {
            Control surface = sender as Control;

            int screenWidth = surface.ClientSize.Width;
            int screenHeight = surface.ClientSize.Height;

            e.Graphics.CompositingMode = Drawing2D.CompositingMode.SourceCopy;
            e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor;

            e.Graphics.DrawImage(
                this._buffer,
                new Drawing.Rectangle(0, 0, screenWidth, screenHeight));

            this.ScreenOffset = Vector2.Zero;
        }
        #endregion
    }
}
