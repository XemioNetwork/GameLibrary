using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Input.Events
{
    public class InputStateEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputStateEvent" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="state">The new state.</param>
        /// <param name="playerIndex">The player index.</param>
        public InputStateEvent(string id, InputState state, int playerIndex)
        {
            this.Id = id;
            this.State = state;
            this.PlayerIndex = playerIndex;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public InputState State { get; private set; }
        /// <summary>
        /// Gets or sets the index of the player.
        /// </summary>
        public int PlayerIndex { get; set; }
        #endregion
    }
}
