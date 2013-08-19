﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Sprites
{
    public static class SpriteSheetOverloads
    {
        #region Methods
        /// <summary>
        /// Renders the specified sprite sheet.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="spriteSheet">The sprite sheet.</param>
        /// <param name="spriteIndex">Index of the sprite.</param>
        /// <param name="position">The position.</param>
        public static void Render(this IRenderManager renderManager, SpriteSheet spriteSheet, int spriteIndex, Vector2 position)
        {
            renderManager.Render(spriteSheet, spriteIndex, position, Color.White);
        }
        /// <summary>
        /// Renders the specified sprite sheet.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="spriteSheet">The sprite sheet.</param>
        /// <param name="spriteIndex">Index of the sprite.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public static void Render(this IRenderManager renderManager, SpriteSheet spriteSheet, int spriteIndex, Vector2 position, Color color)
        {
            renderManager.Render(spriteSheet, spriteIndex, new Rectangle(position.X, position.Y, spriteSheet.FrameWidth, spriteSheet.FrameHeight), color);
        }
        /// <summary>
        /// Renders the specified sprite sheet.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="spriteSheet">The sprite sheet.</param>
        /// <param name="spriteIndex">Index of the sprite.</param>
        /// <param name="destination">The destination.</param>
        public static void Render(this IRenderManager renderManager, SpriteSheet spriteSheet, int spriteIndex, Rectangle destination)
        {
            renderManager.Render(spriteSheet, spriteIndex, destination, Color.White);
        }
        /// <summary>
        /// Renders the specified sprite sheet.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="spriteSheet">The sprite sheet.</param>
        /// <param name="spriteIndex">Index of the sprite.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="color">The color.</param>
        public static void Render(this IRenderManager renderManager, SpriteSheet spriteSheet, int spriteIndex, Rectangle destination, Color color)
        {
            renderManager.Render(spriteSheet, spriteIndex, destination, new Rectangle(0, 0, spriteSheet.FrameWidth, spriteSheet.FrameHeight), color);
        }
        /// <summary>
        /// Renders the specified sprite sheet.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="spriteSheet">The sprite sheet.</param>
        /// <param name="spriteIndex">Index of the sprite.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public static void Render(this IRenderManager renderManager, SpriteSheet spriteSheet, int spriteIndex, Rectangle destination, Rectangle origin)
        {
            renderManager.Render(spriteSheet, spriteIndex, destination, origin, Color.White);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="spriteSheet">The sprite sheet.</param>
        /// <param name="spriteIndex">Index of the sprite.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="color">The color.</param>
        public static void Render(this IRenderManager renderManager, SpriteSheet spriteSheet, int spriteIndex, Rectangle destination, Rectangle origin, Color color)
        {
            Rectangle spriteSource = spriteSheet.GetSourceRectagle(spriteIndex);

            float x = MathHelper.Clamp(spriteSource.X + origin.X, spriteSource.X, spriteSource.X + spriteSource.Width);
            float y = MathHelper.Clamp(spriteSource.Y + origin.Y, spriteSource.Y, spriteSource.Y + spriteSource.Height);

            Rectangle calculatedOrigin = new Rectangle(x, y, 
                MathHelper.Clamp(origin.Width, 0, spriteSource.Width - (x - spriteSource.X)),
                MathHelper.Clamp(origin.Height, 0, spriteSource.Height - (y - spriteSource.Y)));

            renderManager.Render(spriteSheet.Texture, destination, calculatedOrigin, color);
        }
        #endregion
    }
}
