﻿using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input.Events
{
    public class MousePositionEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MousePositionEvent" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="playerIndex">The player index.</param>
        public MousePositionEvent(Vector2 position, int playerIndex)
        {
            this.Position = position;
            this.PlayerIndex = playerIndex;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        #endregion

        #region Implementation of IInputEvent
        /// <summary>
        /// Applies this event to the specified player input.
        /// </summary>
        /// <param name="playerInput">The player input.</param>
        public void Apply(PlayerInput playerInput)
        {
            playerInput.MousePosition = this.Position;
        }
        #endregion
    }
}