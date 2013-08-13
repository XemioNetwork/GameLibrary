using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.Textures;

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
        internal SpriteFont() : this(0)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteFont"/> class.
        /// </summary>
        /// <param name="kerning">The kerning.</param>
        internal SpriteFont(int kerning)
        {
            this.Kerning = kerning;
            this.Spacing = 15;

            this.Textures = new ITexture[byte.MaxValue];
            this.Bitmaps = new Bitmap[byte.MaxValue];
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

            foreach (string value in lines)
            {
                float x = 0;
                float y = int.MinValue;

                foreach (char character in value)
                {
                    x += this.Textures[character].Width;
                    x += this.Kerning;

                    if (character == ' ')
                    {
                        x += this.Spacing;
                    }

                    if (y < this.Textures[character].Height)
                    {
                        y = this.Textures[character].Height;
                    }
                }

                result += new Vector2(x, y + this.Kerning);
            }

            return result;
        }
        #endregion
    }
}
