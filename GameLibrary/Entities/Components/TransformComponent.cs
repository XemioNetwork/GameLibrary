using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Components
{
    public class TransformComponent : EntityComponent
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        public Vector2 Scale { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets the absolute position (Entity position + all position modifiers).
        /// </summary>
        [Exclude]
        public Vector2 AbsolutePosition
        {
            get
            {
                var components = this.Entity.Components.OfType<IPositionModifier>();
                Func<Vector2, IPositionModifier, Vector2> lamda = (current, modifier) => current + modifier.Offset;

                return components.Aggregate(this.Position, lamda);
            }
        }
        #endregion
    }
}
