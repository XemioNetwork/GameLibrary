using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.TileEngine.Tiles;

namespace Xemio.GameLibrary.TileEngine
{
    public class MapParser : IParser<string, Map>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapParser"/> class.
        /// </summary>
        public MapParser()
        {
            this._factory = XGL.GetComponent<ITextureFactory>();
            this._implementations = XGL.GetComponent<ImplementationManager>();
        }
        #endregion

        #region Fields
        private readonly ITextureFactory _factory;
        private readonly ImplementationManager _implementations;
        #endregion

        #region Implementation of IParser<in string,out Map>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public Map Parse(string input)
        {
            JObject jsonMap = JObject.Parse(input);

            int width = jsonMap["width"].Value<int>();
            int height = jsonMap["height"].Value<int>();

            int layers = jsonMap["layers"].Count(t => t["type"].Value<string>() == "tilelayer");

            JArray jsonTilesets = jsonMap["tilesets"].Value<JArray>();
            List<TileSet> tileSets = new List<TileSet>();

            foreach (JObject jsonTileset in jsonTilesets)
            {
                if (jsonTileset["properties"]["IsEntity"] == null)
                {
                    string tilesetName = jsonTileset["name"].Value<string>();
                    string fileName = jsonTileset["image"].Value<string>();
                    int tileWidth = jsonTileset["tilewidth"].Value<int>();
                    int tileHeight = jsonTileset["tileheight"].Value<int>();

                    TileSet tileSet = new TileSet(
                        tilesetName,
                        this._factory.CreateTexture(fileName),
                        tileWidth,
                        tileHeight);

                    JObject jsonTileProperties = jsonTileset["tileproperties"].Value<JObject>();
                    for (int i = 0; i < tileSet.Sheet.Textures.Length; i++)
                    {
                        string tileId = "Default";
                        string namedIndex = i.ToString(CultureInfo.InvariantCulture);

                        JToken currentValue;
                        if (jsonTileProperties.TryGetValue(namedIndex, out currentValue))
                        {
                            tileId = currentValue["TileId"].Value<string>();
                        }

                        Tile tile = this._implementations.Get<string, Tile>(tileId);
                        tileSet.Tiles.Add(tile);
                    }

                    tileSets.Add(tileSet);
                }
            }

            Map map = new Map(tileSets, width, height, layers);



            return map;
        }
        #endregion
    }
}
