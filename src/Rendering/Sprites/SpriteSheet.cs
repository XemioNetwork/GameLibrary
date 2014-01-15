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
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheet"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="frameWidth">Width of the frame.</param>
        /// <param name="frameHeight">Height of the frame.</param>
        public SpriteSheet(ITexture texture, int frameWidth, int frameHeight)
            : this(frameWidth, frameHeight)
        {
            this.Columns = texture.Width / frameWidth;
            this.Rows = texture.Height / frameHeight;

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
        public ITexture Texture { get; private set; }
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
        #endregion
    }
}
