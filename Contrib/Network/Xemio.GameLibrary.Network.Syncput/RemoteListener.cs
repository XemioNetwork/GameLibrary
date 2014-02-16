using System;
using System.Linq;
using System.Collections.Generic;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Syncput.Packages;

namespace Xemio.GameLibrary.Network.Syncput
{
    public class RemoteListener : IInputListener
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteListener"/> class.
        /// </summary>
        public RemoteListener()
        {
            this._packageQueue = new Queue<TurnPackage>();
        }
        #endregion

        #region Fields
        private readonly Queue<TurnPackage> _packageQueue;
        #endregion
        
        #region Event Handlers
        /// <summary>
        /// Handles all turn packages for the specified player index the turn package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Add(TurnPackage package)
        {
            this._packageQueue.Enqueue(package);
        }
        #endregion

        #region Implementation of IInputListener
        /// <summary>
        /// Gets or sets the index of the player.
        /// </summary>
        public int? PlayerIndex { get; set; }
        /// <summary>
        /// Called when the input gets attached to a player.
        /// </summary>
        public void OnAttached()
        {
        }
        /// <summary>
        /// Called when the input gets attached to a player.
        /// </summary>
        public void OnDetached()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this instance can simulate the next turn.
        /// </summary>
        /// <param name="turnSequence">The turn sequence.</param>
        /// <returns>
        ///   <c>true</c> if this instance can simulate the specified turn sequence; otherwise, <c>false</c>.
        /// </returns>
        public bool CanSimulate(int turnSequence)
        {
            return this.PlayerIndex.HasValue &&
                this._packageQueue.Count > 0 &&
                this._packageQueue.Any(p => p.TurnSequence >= turnSequence);
        }
        /// <summary>
        /// Simulates the next turn.
        /// </summary>
        /// <param name="turnSequence">The turn sequence.</param>
        public void Simulate(int turnSequence)
        {
            var playerIndex = this.PlayerIndex.Value;

            var eventManager = XGL.Components.Get<EventManager>();
            var package = this._packageQueue.First();

            if (package.TurnSequence > turnSequence)
                return;

            foreach (KeyValuePair<Keys, InputState> keyState in package.KeyStates)
            {
                var e = new InputStateEvent(keyState.Key, keyState.Value, playerIndex);
                eventManager.Publish(e);
            }

            eventManager.Publish(new MousePositionEvent(package.MousePosition, playerIndex));

            this._packageQueue.Dequeue();
        }
        #endregion
    }
}
