using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Logic.Forwarding;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Syncput.Packages.Requests;

namespace Xemio.GameLibrary.Network.Syncput.Packages
{
    public class TurnPackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnPackage"/> class.
        /// </summary>
        public TurnPackage()
        {
            this.KeyStates = new Dictionary<Keys, InputState>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnPackage"/> class.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="request">The request.</param>
        public TurnPackage(int playerIndex, TurnRequest request) 
            : this((byte)playerIndex, request.TurnSequence, request.KeyStates, request.MousePosition)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnPackage"/> class.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="turnSequence">The turn sequence.</param>
        /// <param name="keyStates">The key states.</param>
        /// <param name="mousePosition">The mouse position.</param>
        public TurnPackage(byte playerIndex, int turnSequence, Dictionary<Keys, InputState> keyStates, Vector2 mousePosition )
        {
            this.PlayerIndex = playerIndex;
            this.TurnSequence = turnSequence;
            this.KeyStates = keyStates;
            this.MousePosition = mousePosition;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the index of the player.
        /// </summary>
        public byte PlayerIndex { get; private set; }
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
