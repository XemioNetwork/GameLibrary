using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering.Fonts
{
    public class SpriteFontRasterizer : ITextRasterizer
    {
        #region Implementation of ITextRasterizer
        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="value">The value.</param>
        /// <param name="position">The position.</param>
        public void Render(IFont font, string value, Vector2 position)
        {
            var spriteFont = (SpriteFont)font;

            var graphicsDevice = XGL.Components.Require<GraphicsDevice>();
            
            string[] lines = value.Replace("\r", string.Empty).Split('\n');
            Vector2 currentPosition = position;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    currentPosition.Y += spriteFont.Spacing;
                    continue;
                }

                int maximumHeight = int.MinValue;

                foreach (char character in line)
                {
                    int index = character;

                    ITexture texture = spriteFont.Textures[index];
                    if (texture == null)
                    {
                        throw new InvalidOperationException("Cannot render character " + character + " (" + index + ").");
                    }

                    graphicsDevice.Render(texture, new Rectangle(currentPosition.X, currentPosition.Y, texture.Width, texture.Height));

                    if (character == ' ')
                    {
                        currentPosition.X += spriteFont.Spacing;
                    }
                    if (texture.Height > maximumHeight)
                    {
                        maximumHeight = texture.Height;
                    }

                    currentPosition.X += texture.Width + spriteFont.Kerning;
                }

                currentPosition.X = position.X;
                currentPosition += new Vector2(0, maximumHeight + spriteFont.Kerning + 2);
            }
        }
        #endregion
    }
}
