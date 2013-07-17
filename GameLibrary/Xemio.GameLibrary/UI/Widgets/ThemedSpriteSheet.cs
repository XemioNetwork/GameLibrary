using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.GameLibrary.UI.Widgets
{
    public class ThemedSpriteSheet
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemedSpriteSheet"/> class.
        /// </summary>
        /// <param name="spriteSheet">The sprite sheet.</param>
        public ThemedSpriteSheet(SpriteSheet spriteSheet)
        {
            this._spriteSheet = spriteSheet;
        }
        #endregion

        #region Fields
        private readonly SpriteSheet _spriteSheet;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the top left texture.
        /// </summary>
        public ITexture TopLeft
        {
            get { return this._spriteSheet.Textures[0]; }
        }
        /// <summary>
        /// Gets the top texture.
        /// </summary>
        public ITexture Top
        {
            get { return this._spriteSheet.Textures[1]; }
        }
        /// <summary>
        /// Gets the top right texture.
        /// </summary>
        public ITexture TopRight
        {
            get { return this._spriteSheet.Textures[2]; }
        }
        /// <summary>
        /// Gets the left texture.
        /// </summary>
        public ITexture Left
        {
            get { return this._spriteSheet.Textures[3]; }
        }
        /// <summary>
        /// Gets the center texture.
        /// </summary>
        public ITexture Center
        {
            get { return this._spriteSheet.Textures[4]; }
        }
        /// <summary>
        /// Gets the right texture.
        /// </summary>
        public ITexture Right
        {
            get { return this._spriteSheet.Textures[5]; }
        }
        /// <summary>
        /// Gets the bottom left texture.
        /// </summary>
        public ITexture BottomLeft
        {
            get { return this._spriteSheet.Textures[6]; }
        }
        /// <summary>
        /// Gets the bottom texture.
        /// </summary>
        public ITexture Bottom
        {
            get { return this._spriteSheet.Textures[7]; }
        }
        /// <summary>
        /// Gets the bottom right texture.
        /// </summary>
        public ITexture BottomRight
        {
            get { return this._spriteSheet.Textures[8]; }
        }
        #endregion
    }
}
