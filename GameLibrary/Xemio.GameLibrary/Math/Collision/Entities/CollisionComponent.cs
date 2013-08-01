using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Entities.Events;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math.Collision.Events;
using Xemio.GameLibrary.Math.Collision.Sources;

namespace Xemio.GameLibrary.Math.Collision.Entities
{
    public class CollisionComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="collisionMap">The collision map.</param>
        public CollisionComponent(CollidableEntity entity, CollisionMap collisionMap) : base(entity)
        {
            this._eventManager = XGL.Components.Get<EventManager>();
            this._eventManager.Subscribe<EntityPositionChangedEvent>(this.PositionChanged);

            this.CollisionMap = collisionMap;
        }
        #endregion

        #region Fields
        private bool _activeCollisionCheck;
        private readonly EventManager _eventManager;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collision map.
        /// </summary>
        public CollisionMap CollisionMap { get; private set; }
        /// <summary>
        /// Gets the collidable entity.
        /// </summary>
        public CollidableEntity CollidableEntity
        {
            get { return this.Entity as CollidableEntity; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Fires the collision events.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="environment">The environment.</param>
        private void FireCollisionEvents(int x, int y, CollisionEnvironment environment)
        {
            for (int i = 0; i < this.CollisionMap.Width; i++)
            {
                for (int j = 0; j < this.CollisionMap.Height; j++)
                {
                    if (!this.CollisionMap.Cells[i, j])
                        continue;

                    IList<ICollisionSource> sources = environment.Grid.GetCell(x + i, y + j);
                    foreach (ICollisionSource source in sources)
                    {
                        if (source == this.Entity)
                            continue;
                        
                        var collisionEvent = new CollisionEvent(this.CollidableEntity, source);
                        this._eventManager.Publish(collisionEvent);
                    }
                }
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles an entity position change.
        /// </summary>
        /// <param name="e">The event.</param>
        private void PositionChanged(EntityPositionChangedEvent e)
        {
            if (this._activeCollisionCheck)
                return;

            this._activeCollisionCheck = true;
            if (e.Entity == this.Entity && this.CollidableEntity.IsDirty)
            {
                var environment = this.CollidableEntity.Environment as CollisionEnvironment;
                if (environment != null)
                {
                    this.CollidableEntity.Position -= e.Delta;

                    Vector2 direction = Vector2.Normalize(e.Delta);
                    float[] dimensionLengths = { MathHelper.Abs(e.Delta.X), MathHelper.Abs(e.Delta.Y) };

                    Vector2[] dimensions = {
                        new Vector2(direction.X, 0),
                        new Vector2(0, direction.Y)
                    };

                    for (int i = 0; i < dimensions.Length; i++)
                    {
                        float length = dimensionLengths[i];

                        while (length > 0)
                        {
                            float multiplier = environment.Grid.CellSize;

                            if (length < environment.Grid.CellSize)
                                multiplier = length;

                            this.CollidableEntity.Position += dimensions[i] * multiplier;

                            environment.Grid.Update(
                                this.CollidableEntity,
                                this.CollisionMap);

                            int x = (int)this.CollidableEntity.Position.X / environment.Grid.CellSize;
                            int y = (int)this.CollidableEntity.Position.Y / environment.Grid.CellSize;

                            if (environment.Grid.Collides(x, y, this.CollidableEntity, this.CollisionMap))
                            {
                                this.CollidableEntity.Position -= dimensions[i] * multiplier;
                                this.FireCollisionEvents(x, y, environment);

                                break;
                            }

                            length -= environment.Grid.CellSize;
                        }
                    }
                }
            }

            this._activeCollisionCheck = false;
        }
        #endregion
    }
}
