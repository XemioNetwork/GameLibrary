using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.TileEngine.Tiles;

namespace Xemio.GameLibrary.TileEngine
{
    /// <summary>
    /// Represents a field on the map.
    /// </summary>
    public class Field
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="layer">The layer.</param>
        public Field(Map map, int x, int y, int layer)
        {
            this.Map = map;
            this.X = x;
            this.Y = y;
            this.Layer = layer;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the X.
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Gets the Y.
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Gets the layer.
        /// </summary>
        public int Layer { get; private set; }
        /// <summary>
        /// Gets the map.
        /// </summary>
        public Map Map { get; private set; }
        /// <summary>
        /// Gets or sets the tile.
        /// </summary>
        public Tile Tile { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the tile.
        /// </summary>
        /// <param name="tileId">The tile id.</param>
        public void SetTile(string tileId)
        {
            var implementations = XGL.GetComponent<ImplementationManager>();
            this.Tile = implementations.Get<string, Tile>(tileId);
        }
        #endregion
    }
}
