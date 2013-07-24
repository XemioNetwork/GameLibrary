using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math.Collision.Sources;

namespace Xemio.GameLibrary.Math.Collision
{
    public class CollisionGrid
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionGrid"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="cellSize">Size of the cell.</param>
        public CollisionGrid(int width, int height, int cellSize)
        {
            CellSizeHelper.ValidateCellSize(cellSize);

            this.Width = width;
            this.Height = height;

            this.CellSize = cellSize;
            this.Cells = new List<ICollisionSource>[width, height];

            this._positionMappings = new Dictionary<ICollisionSource, Vector2>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<ICollisionSource, Vector2> _positionMappings; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Gets the size of the cell.
        /// </summary>
        public int CellSize { get; private set; }
        /// <summary>
        /// Gets the cells.
        /// </summary>
        public List<ICollisionSource>[,] Cells { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Performs an action for the specified collection of collision sources.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="source">The source.</param>
        /// <param name="collisionMap">The collision map.</param>
        /// <param name="action">The action.</param>
        private void PerformAction(int x, int y, ICollisionSource source, CollisionMap collisionMap, Action<IList<ICollisionSource>> action)
        {
            for (int i = 0; i < collisionMap.Width; i++)
            {
                for (int j = 0; j < collisionMap.Height; j++)
                {
                    if (collisionMap.Cells[i, j] &&
                        x + i < this.Width &&
                        x + i >= 0 &&
                        y + j < this.Height &&
                        y + j >= 0)
                    {
                        action(this.GetCell(x + i, y + j));
                    }
                }
            }
        }
        /// <summary>
        /// Adds the specified source if it doesn't exist. Otherwise it gets removed first.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="collisionMap">The collision map.</param>
        public void Update(ICollisionSource source, CollisionMap collisionMap)
        {
            int x = (int)source.Position.X / this.CellSize;
            int y = (int)source.Position.Y / this.CellSize;

            if (this._positionMappings.ContainsKey(source))
                this.Remove(source, collisionMap);
            
            this.PerformAction(x, y,
                source,
                collisionMap,
                list => list.Add(source));

            this._positionMappings.Add(source, source.Position);
        }
        /// <summary>
        /// Removes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="collisionMap">The collision map.</param>
        public void Remove(ICollisionSource source, CollisionMap collisionMap)
        {
            if (this._positionMappings.ContainsKey(source))
            {
                Vector2 position = this._positionMappings[source];

                int x = (int)position.X / this.CellSize;
                int y = (int)position.Y / this.CellSize;

                this.PerformAction(x, y,
                    source,
                    collisionMap,
                    list => list.Remove(source));

                this._positionMappings.Remove(source);
            }
        }
        /// <summary>
        /// Checks for a collision on the specified location.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="checkingSource">The checking source.</param>
        /// <param name="collisionMap">The collision map.</param>
        /// <returns></returns>
        public bool Collides(int x, int y, ICollisionSource checkingSource, CollisionMap collisionMap)
        {
            for (int i = 0; i < collisionMap.Width; i++)
            {
                for (int j = 0; j < collisionMap.Height; j++)
                {
                    if (!collisionMap.Cells[i, j])
                        continue;

                    IList<ICollisionSource> sources = this.GetCell(x + i, y + j);
                    if (sources.Any(source => source != checkingSource))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Determines whether the specified coordinate inside the grid has collision sources.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public bool Collides(int x, int y)
        {
            var cell = this.GetCell(x, y);
            if (cell == null)
            {
                return false;
            }

            return this.GetCell(x, y).Count > 0;
        }
        /// <summary>
        /// Gets the cell at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public IList<ICollisionSource> GetCell(int x, int y)
        {
            if (x < 0 || x >= this.Width ||
                y < 0 || y >= this.Height)
            {
                return new List<ICollisionSource>()
                           {
                               new StaticCollisionSource(x * this.CellSize, y * this.CellSize)
                           };
            }

            if (this.Cells[x, y] == null)
                this.Cells[x, y] = new List<ICollisionSource>();

            return this.Cells[x, y];
        }
        #endregion
    }
}
