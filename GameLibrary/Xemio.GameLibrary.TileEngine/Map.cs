using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.TileEngine.Tiles;

namespace Xemio.GameLibrary.TileEngine
{
    public class Map
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="tileSets">The tile sets.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="layers">The layers.</param>
        public Map(IEnumerable<TileSet> tileSets, int width, int height, int layers)
        {
            this.TileSets = tileSets.ToList();
            this.Fields = new Field[width, height, layers];
            this.Environment = new EntityEnvironment();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tile sets.
        /// </summary>
        public List<TileSet> TileSets { get; private set; }
        /// <summary>
        /// Gets the fields.
        /// </summary>
        public Field[,,] Fields { get; private set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        #endregion
    }
}
