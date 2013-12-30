using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Content.Attributes;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Components
{
    public class PositionComponent : EntityComponent
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private Vector2 _value;
        private bool _resetChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Value
        {
            get { return this._value; }
            set
            {
                if (value != this._value)
                {
                    logger.Trace("Setting position for entity {0}-{1} to {2}", this.GetType().Name, this.Entity.Guid, this.Value);

                    this.HasChanged = true;
                    this._value = value;
                }
            }
        }
        /// <summary>
        /// Gets the absolute position (Entity position + all position modifiers).
        /// </summary>
        [ExcludeSerialization]
        public Vector2 Absolute
        {
            get { return this.Entity.Components.OfType<IPositionComponent>().Aggregate(this.Value, (current, modifier) => current + modifier.Offset); }
        }
        /// <summary>
        /// Gets a value indicating whether the position has changed.
        /// </summary>
        [ExcludeSerialization]
        public bool HasChanged { get; private set; }
        #endregion

        #region Overrides of EntityComponent
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this._resetChanged)
            {
                this.HasChanged = false;
                this._resetChanged = false;
            }
            if (!this._resetChanged && this.HasChanged)
            {
                this._resetChanged = true;
            }
        }
        #endregion
    }
}
