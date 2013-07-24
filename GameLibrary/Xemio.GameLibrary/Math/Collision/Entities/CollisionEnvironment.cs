using System;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math.Collision.Events;
using Xemio.GameLibrary.Math.Collision.Sources;

namespace Xemio.GameLibrary.Math.Collision.Entities
{
    public class CollisionEnvironment : EntityEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionEnvironment"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="cellSize">Size of the cell.</param>
        public CollisionEnvironment(int width, int height, int cellSize)
        {
            this.Grid = new CollisionGrid(width, height, cellSize);
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the grid.
        /// </summary>
        public CollisionGrid Grid { get; private set; }
        #endregion
    }
}
