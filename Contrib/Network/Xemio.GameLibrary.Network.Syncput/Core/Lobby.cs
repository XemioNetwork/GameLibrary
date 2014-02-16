using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Syncput.Packages;

namespace Xemio.GameLibrary.Network.Syncput.Core
{
    public class Lobby : IEnumerable<Player>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Lobby"/> class.
        /// </summary>
        public Lobby()
        {
            this.MaxPlayers = 4;

            this._idMappings = new Dictionary<int, int>();
            this._players = new List<Player>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<int, int> _idMappings;
        private readonly IList<Player> _players; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the max players.
        /// </summary>
        public int MaxPlayers { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Lobby"/> is ready.
        /// </summary>
        public bool Ready { get; internal set; }
        /// <summary>
        /// Gets the player count.
        /// </summary>
        public int Count
        {
            get { return this._players.Count; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is full.
        /// </summary>
        public bool IsFull
        {
            get { return this.Count >= this.MaxPlayers; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the input for the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        private void CreateInput(Player player)
        {
            var inputManager = XGL.Components.Get<InputManager>();
            while (player.PlayerIndex >= inputManager.PlayerInputs.Count)
            {
                inputManager.CreateInput();
            }

            inputManager.AddListener(new RemoteListener(), player.PlayerIndex);
        }
        /// <summary>
        /// Creates the player.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="indexInequality">The index inequality.</param>
        internal Player CreatePlayer(string name, long indexInequality)
        {
            var inputManager = XGL.Components.Get<InputManager>();

            return new Player(
                this.Count,
                inputManager.PlayerInputs.Count, 
                indexInequality, 
                name);
        }
        /// <summary>
        /// Creates a player with the specified name and adds it to the the lobby.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="indexInequality">The index inequality.</param>
        internal void Add(string name, long indexInequality)
        {
            this.Add(this.CreatePlayer(name, indexInequality));
        }
        /// <summary>
        /// Adds the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        internal void Add(Player player)
        {
            if (!this.IsFull && !this._players.Contains(player))
            {
                this._players.Add(player);
                this._idMappings.Add(player.Id, player.PlayerIndex);

                this.CreateInput(player);
            }
        }
        /// <summary>
        /// Removes the specified player.
        /// </summary>
        /// <param name="playerId">The player id.</param>
        internal void Remove(int playerId)
        {
            var player = this._players.First(p => p.Id == playerId);
            var inputManager = XGL.Components.Get<InputManager>();

            if (inputManager.PlayerInputs.Count > player.PlayerIndex)
            {
                inputManager.PlayerInputs.RemoveAt(player.PlayerIndex);
            }

            this._players.Remove(player);
            this._idMappings.Remove(player.Id);
        }
        /// <summary>
        /// Gets the player index for the specified id.
        /// </summary>
        /// <param name="playerId">The player id.</param>
        public int GetPlayerIndex(int playerId)
        {
            return this._idMappings[playerId];
        }
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Player> GetEnumerator()
        {
            return this._players.GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
