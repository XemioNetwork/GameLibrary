using System;

namespace Xemio.GameLibrary.Network.Syncput.Core
{
    public class Player : IEquatable<Player>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="indexInequality">The index inequality.</param>
        /// <param name="name">The name.</param>
        public Player(int id, int playerIndex, long indexInequality, string name)
        {
            this.Id = id;
            this.PlayerIndex = playerIndex;
            this.IndexInequality = indexInequality;
            this.Name = name;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        /// <summary>
        /// Gets the index inequality.
        /// </summary>
        public long IndexInequality { get; private set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        #endregion
        
        #region Overrides of Object
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return this.Id;
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        public override bool Equals(object obj)
        {
            Player player = obj as Player;

            if (player == null)
                return false;

            return this.Equals(player);
        }
        #endregion

        #region Implementation of IEquatable<Player>
        /// <summary>
        /// Determines whether the instance equals this instance.
        /// </summary>
        public bool Equals(Player other)
        {
            return this.Id == other.Id;
        }
        #endregion
    }
}
