using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Drawing;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using System.Drawing.Imaging;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Rendering.Sprites
{
    using Drawing = System.Drawing;
    using Drawing2D = System.Drawing.Drawing2D;

    using Rectangle = Math.Rectangle;

    public class SpriteSheet
    {
        #region Fields
        private readonly Dictionary<int, ITexture> _textureCache = new Dictionary<int, ITexture>();
        private readonly Image _sourceImage;
        #endregion 

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet" /> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        public SpriteSheet(string fileName, int frameWidth, int frameHeight)
            : this(XGL.Components.Get<IFileSystem>().Open(fileName), frameWidth, frameHeight)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        public SpriteSheet(Stream stream, int frameWidth, int frameHeight)
            : this(frameWidth, frameHeight)
        {
            this._sourceImage = Image.FromStream(stream);

            this.Columns = this._sourceImage.Width / frameWidth;
            this.Rows = this._sourceImage.Height / frameHeight;

            this.Texture = XGL.Components.Get<ContentManager>().Load<ITexture>(stream);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        public SpriteSheet(ITexture texture, int frameWidth, int frameHeight)
            : this(frameWidth, frameHeight)
        {
            byte[] data = texture.GetData();
            MemoryStream stream = new MemoryStream(data);

            this._sourceImage = Image.FromStream(stream);

            this.Columns = this._sourceImage.Width / frameWidth;
            this.Rows = this._sourceImage.Height / frameHeight;

            this.Texture = texture;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet"/> class.
        /// </summary>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        private SpriteSheet(int frameWidth, int frameHeight)
        {
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the width of the frame.
        /// </summary>
        public int FrameWidth { get; private set; }
        /// <summary>
        /// Gets or sets the height of the frame.
        /// </summary>
        public int FrameHeight { get; private set; }
        /// <summary>
        /// Gets the columns.
        /// </summary>
        public int Columns { get; private set; }
        /// <summary>
        /// Gets the rows.
        /// </summary>
        public int Rows { get; private set; }
        /// <summary>
        /// Gets the texture.
        /// </summary>
        internal ITexture Texture { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the source rectagle for the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public Rectangle GetSourceRectagle(int index)
        {
            int x = index % this.Columns;
            int y = index / this.Columns;

            return new Rectangle(x * this.FrameWidth, y * this.FrameHeight, this.FrameWidth, this.FrameHeight);
        }
        /// <summary>
        /// Returns the texture at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public ITexture GetTexture(int index)
        {
            if (this._textureCache.ContainsKey(index))
                return this._textureCache[index];

            int x = index % this.Columns;
            int y = index / this.Columns;

            Bitmap frame = new Bitmap(this.FrameWidth, this.FrameHeight);
            using (Graphics frameGraphics = Graphics.FromImage(frame))
            {
                frameGraphics.CompositingMode = CompositingMode.SourceOver;
                frameGraphics.CompositingQuality = CompositingQuality.HighSpeed;
                frameGraphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor;

                frameGraphics.DrawImage(this._sourceImage,
                    new Drawing.Rectangle(-x * this.FrameWidth, -y * this.FrameHeight, this._sourceImage.Width, this._sourceImage.Height));
            }

            MemoryStream frameStream = new MemoryStream();

            frame.Save(frameStream, ImageFormat.Png);
            frameStream.Seek(0, SeekOrigin.Begin);

            ContentManager content = XGL.Components.Get<ContentManager>();
            ITexture texture = content.Load<ITexture>(frameStream);

            this._textureCache.Add(index, texture);

            return texture;
        }
        #endregion
    }
}
