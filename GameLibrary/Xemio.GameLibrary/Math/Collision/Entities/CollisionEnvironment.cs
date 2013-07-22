using System;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math.Collision.Events;

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

        #region Methods
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            var eventManager = XGL.Components.Get<EventManager>();

            var dirtyEntities = this.Entities
                .Where(e => e.IsDirty)
                .Where(e => e.GetComponent<CollisionComponent>() != null)
                .ToList();

            foreach (Entity entity in dirtyEntities)
            {
                this.Grid.Update(entity, entity
                    .GetComponent<CollisionComponent>()
                    .CollisionMap);
            }

            foreach (Entity entity in dirtyEntities)
            {
                var collisionComponent = entity.GetComponent<CollisionComponent>();
                var collisionMap = collisionComponent.CollisionMap;
                
                int x = (int)entity.Position.X / this.Grid.CellSize;
                int y = (int)entity.Position.Y / this.Grid.CellSize;

                for (int i = 0; i < collisionMap.Width; i++)
                {
                    for (int j = 0; j < collisionMap.Height; j++)
                    {
                        if (!collisionMap.Cells[i, j])
                            continue;

                        IList<ICollisionSource> sources = this.Grid.GetCell(x + i, y + j);
                        foreach (ICollisionSource source in sources)
                        {
                            if (source == entity)
                                continue;

                            CollisionType type = dirtyEntities.Contains(source)
                                                     ? CollisionType.Both
                                                     : CollisionType.Single;

                            CollisionEvent collisionEvent = new CollisionEvent(entity, source, type);
                            eventManager.Publish(collisionEvent);
                        }
                    }
                }
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
