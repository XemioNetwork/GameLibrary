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
    public static class SpriteFontRenderer
    {
        #region Methods
        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="font">The font.</param>
        /// <param name="value">The value.</param>
        /// <param name="position">The position.</param>
        public static void Render(this IRenderManager renderManager, SpriteFont font, string value, Vector2 position)
        {
            string[] lines = value.Split('\n');
            Vector2 currentPosition = position;

            foreach (string line in lines)
            {
                int maximumHeight = int.MinValue;

                foreach (char character in line)
                {
                    int index = character;

                    ITexture texture = font.Textures[index];
                    if (texture == null)
                    {
                        throw new InvalidOperationException(
                            "Cannot render character " + character + " (" + index + ").");
                    }

                    renderManager.Render(
                        texture,
                        new Rectangle(
                            currentPosition.X,
                            currentPosition.Y,
                            texture.Width,
                            texture.Height));

                    currentPosition.X += texture.Width + font.Kerning;
                    currentPosition.X += character == ' ' ? font.Spacing : 0;

                    if (texture.Height > maximumHeight)
                    {
                        maximumHeight = texture.Height;
                    }
                }

                currentPosition.X = position.X;
                currentPosition += new Vector2(0, maximumHeight + font.Kerning + 2);
            }
        }
        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="font">The font.</param>
        /// <param name="value">The value.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public static void Render(this IRenderManager renderManager, SpriteFont font, string value, Vector2 position, Color color)
        {
            renderManager.Tint(color);

            renderManager.Render(font, value, position);
            renderManager.Tint(Color.White);
        }
        #endregion
    }
}
