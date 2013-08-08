using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Input.Keyboard;
using Xemio.GameLibrary.Input.Mouse;
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
        public InputDevice<Keys> Keyboard { get; private set; }
        /// <summary>
        /// Gets the mouse states.
        /// </summary>
        public InputDevice<MouseButtons> Mouse { get; private set; }
        /// <summary>
        /// Gets or sets the mouse position.
        /// </summary>
        public Vector2 MousePosition { get; internal set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInput"/> class.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public PlayerInput(int playerIndex)
        {
            this.PlayerIndex = playerIndex;

            this.Keyboard = new InputDevice<Keys>();
            this.Mouse = new InputDevice<MouseButtons>();
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Updates the keyboard and mouse.
        /// </summary>
        public void Update()
        {
            this.Keyboard.UpdateChanges();
            this.Mouse.UpdateChanges();
        }
        #endregion Methods
    }
}
