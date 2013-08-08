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

namespace Xemio.GameLibrary.Rendering.Sprites
{
    public class SpriteSheet
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet" /> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        public SpriteSheet(string fileName, int frameWidth, int frameHeight)
        {
            this.Initialize(File.OpenRead(fileName), frameWidth, frameHeight);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        public SpriteSheet(Stream stream, int frameWidth, int frameHeight)
        {
            this.Initialize(stream, frameWidth, frameHeight);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        public SpriteSheet(ITexture texture, int frameWidth, int frameHeight)
        {
            byte[] data = texture.GetData();
            MemoryStream stream = new MemoryStream(data);

            this.Initialize(stream, frameWidth, frameHeight);
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
        /// Gets or sets the textures.
        /// </summary>
        public ITexture[] Textures { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the sprite sheet.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        private void Initialize(Stream stream, int frameWidth, int frameHeight)
        {
            Image image = Image.FromStream(stream);
            ITextureFactory factory = XGL.Components.Get<ITextureFactory>();

            this.Columns = image.Width / frameWidth;
            this.Rows = image.Height / frameHeight;

            Bitmap[] sprites = new Bitmap[this.Columns * this.Rows];
            for (int y = 0; y < this.Rows; y++)
            {
                for (int x = 0; x < this.Columns; x++)
                {
                    int currentIndex = y * this.Columns + x;
                    Bitmap frame = new Bitmap(frameWidth, frameHeight);

                    using (Graphics frameGraphics = Graphics.FromImage(frame))
                    {
                        frameGraphics.CompositingMode = CompositingMode.SourceOver;
                        frameGraphics.CompositingQuality = CompositingQuality.HighSpeed;
                        frameGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;

                        frameGraphics.DrawImage(image,
                            new Rectangle(-x * frameWidth, -y * frameHeight, image.Width, image.Height));
                    }

                    sprites[currentIndex] = frame;
                }
            }

            this.Textures = new ITexture[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    sprites[i].Save(memory, ImageFormat.Png);
                    memory.Seek(0, SeekOrigin.Begin);

                    this.Textures[i] = factory.CreateTexture(memory);
                }
            }

            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
        }
        #endregion
    }
}
