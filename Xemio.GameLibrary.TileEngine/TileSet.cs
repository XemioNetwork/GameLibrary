using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.GameLibrary.TileEngine
{
    public class TileSet
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TileSet"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="tileWidth">Width of the tile.</param>
        /// <param name="tileHeight">Height of the tile.</param>
        public TileSet(string name, ITexture texture, int tileWidth, int tileHeight)
        {
            this.Tiles = new List<Tile>();
            this.Sheet = new SpriteSheet(texture, tileWidth, tileHeight);

            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the tiles.
        /// </summary>
        public List<Tile> Tiles { get; private set; }
        /// <summary>
        /// Gets the width of the tile.
        /// </summary>
        public int TileWidth { get; private set; }
        /// <summary>
        /// Gets the height of the tile.
        /// </summary>
        public int TileHeight { get; private set; }
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public SpriteSheet Sheet { get; private set; }
        #endregion
    }
}
