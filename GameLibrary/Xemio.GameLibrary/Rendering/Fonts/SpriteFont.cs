using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering.Fonts
{
    using Rectangle = Xemio.GameLibrary.Math.Rectangle;
    using Drawing = System.Drawing;

    public class SpriteFont
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteFont"/> class.
        /// </summary>
        internal SpriteFont()
        {
            this.Spacing = 15;

            this.Textures = new ITexture[byte.MaxValue];
            this.Bitmaps = new Bitmap[byte.MaxValue];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteFont"/> class.
        /// </summary>
        /// <param name="kerning">The kerning.</param>
        internal SpriteFont(int kerning) : this()
        {
            this.Kerning = kerning;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteFont"/> class.
        /// </summary>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="size">The size.</param>
        public SpriteFont(string fontFamily, float size) : this(new Font(fontFamily, size))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteFont"/> class.
        /// </summary>
        /// <param name="font">The font.</param>
        public SpriteFont(Font font) : this()
        {
            Bitmap measureBitmap = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(measureBitmap);

            Brush brush = Brushes.White;
            Bitmap[] bitmaps = new Bitmap[byte.MaxValue];

            for (int i = 31; i < 253; i++)
            {
                char character = (char)i;
                string current = character.ToString();

                SizeF size = graphics.MeasureString(current, font);

                Bitmap letterMap = new Bitmap(
                    (int)size.Width,
                    (int)size.Height,
                    PixelFormat.Format32bppPArgb);

                using (Graphics letterGraphics = Graphics.FromImage(letterMap))
                {
                    letterGraphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                    letterGraphics.DrawString(current, font, brush, PointF.Empty);

                    bitmaps[i] = letterMap;
                }
            }

            this.Initialize(bitmaps);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the space between 2 characters.
        /// </summary>
        public int Kerning { get; set; }
        /// <summary>
        /// Gets or sets the width of the space character.
        /// </summary>
        public int Spacing { get; set; }
        /// <summary>
        /// Gets the textures.
        /// </summary>
        public ITexture[] Textures { get; private set; }
        /// <summary>
        /// Gets the font cache.
        /// </summary>
        internal Bitmap[] Bitmaps { get; set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the spritefont textures.
        /// </summary>
        /// <param name="bitmaps">The bitmaps.</param>
        private void Initialize(Bitmap[] bitmaps)
        {
            var serializer = XGL.Components.Get<SerializationManager>();

            for (int i = 0; i < bitmaps.Length; i++)
            {
                if (bitmaps[i] != null)
                {
                    this.Textures[i] = serializer.Load<ITexture>(bitmaps[i].ToStream());
                    this.Bitmaps[i] = bitmaps[i];
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Measures the specified string.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector2 MeasureString(string value)
        {
            return this.MeasureString(value.Split('\n'));
        }
        /// <summary>
        /// Measures the specified string.
        /// </summary>
        /// <param name="lines">The lines.</param>
        public Vector2 MeasureString(string[] lines)
        {
            Vector2 result = Vector2.Zero;

            for (int yy = 0; yy < lines.Length; yy++)
            {
                string line = lines[yy];

                float x = 0;
                float y = int.MinValue;

                for (int xx = 0; xx < line.Length; xx++)
                {
                    char character = line[xx];

                    x += this.Textures[character].Width;
                    if (xx < line.Length - 1)
                    {
                        x += this.Kerning;
                    }

                    if (character == ' ')
                    {
                        x += this.Spacing;
                    }

                    if (y < this.Textures[character].Height)
                    {
                        y = this.Textures[character].Height;
                    }
                }

                result += new Vector2(x, y);
                if (yy < lines.Length - 1)
                {
                    result += new Vector2(0, this.Kerning);
                }
            }

            return result;
        }
        #endregion
    }
}
