﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.TileEngine
{
    public class DefaultTile : Tile
    {
        #region Overrides of Tile
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public override string Id
        {
            get { return "Default"; }
        }
        #endregion
    }
}
