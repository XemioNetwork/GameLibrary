using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Syncput.Packages.Requests
{
    public class TurnRequest : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnRequest"/> class.
        /// </summary>
        public TurnRequest()
        {
            this.KeyStates = new Dictionary<Keys, InputState>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnRequest"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="turnSequence">The turn sequence.</param>
        public TurnRequest(PlayerInput input, int turnSequence) : this()
        {
            foreach (Keys key in input.GetKeys(key => input.HasState(key) && !this.KeyStates.ContainsKey(key)))
            {
                this.KeyStates.Add(key, input[key]);
            }

            this.MousePosition = input.MousePosition;
            this.TurnSequence = turnSequence;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnRequest"/> class.
        /// </summary>
        /// <param name="turnSequence">The turn sequence.</param>
        /// <param name="keyStates">The key states.</param>
        /// <param name="mousePosition">The mouse position.</param>
        public TurnRequest(int turnSequence, Dictionary<Keys, InputState> keyStates, Vector2 mousePosition)
        {
            this.TurnSequence = turnSequence;
            this.KeyStates = keyStates;
            this.MousePosition = mousePosition;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the turn sequence.
        /// </summary>
        public int TurnSequence { get; private set; }
        /// <summary>
        /// Gets or sets the key states.
        /// </summary>
        public Dictionary<Keys, InputState> KeyStates { get; private set; }
        /// <summary>
        /// Gets or sets the mouse position.
        /// </summary>
        public Vector2 MousePosition { get; private set; }
        #endregion
    }
}
