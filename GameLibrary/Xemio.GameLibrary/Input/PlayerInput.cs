using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input
{
    public class PlayerInput
    {
        #region Properties
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        /// <summary>
        /// Gets the key states.
        /// </summary>
        public InputStorage Storage { get; private set; }
        /// <summary>
        /// Gets or sets the mouse position.
        /// </summary>
        public Vector2 MousePosition { get; internal set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInput"/> class.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public PlayerInput(int playerIndex)
        {
            this.PlayerIndex = playerIndex;
            this.Storage = new InputStorage();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the keyboard and mouse.
        /// </summary>
        public void Update()
        {
            this.Storage.UpdateStates();
        }
        #endregion
    }
}
