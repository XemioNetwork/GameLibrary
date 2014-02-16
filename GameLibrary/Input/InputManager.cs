using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input.Events;

namespace Xemio.GameLibrary.Input
{
    [Require(typeof(IGameLoop))]
    [Require(typeof(IEventManager))]

    public class InputManager : IConstructable, ISortableTickHandler
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly IList<PlayerInput> _inputs; 
        private readonly IList<IInputListener> _listeners;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the local player input.
        /// </summary>
        public PlayerInput LocalInput
        {
            get { return this._inputs.First(); }
        }
        /// <summary>
        /// Gets the <see cref="Xemio.GameLibrary.Input.PlayerInput"/> with the specified player index.
        /// </summary>
        public PlayerInput this[int playerIndex]
        {
            get
            {
                if (!this.IsPlayerIndexValid(playerIndex))
                    return null;

                return this._inputs[playerIndex];
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        public InputManager()
        {
            this._listeners = new List<IInputListener>();
            this._inputs = new List<PlayerInput>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a player input.
        /// </summary>
        public PlayerInput CreateInput()
        {
            logger.Info("Creating player input with id {0}", this._inputs.Count);

            var playerInput = new PlayerInput(this._inputs.Count);
            this._inputs.Add(playerInput);

            return playerInput;
        }
        /// <summary>
        /// Adds the listener to the specified player index.
        /// </summary>
        /// <param name="listener">The listener.</param>
        /// <param name="playerIndex">Index of the player.</param>
        public void AddListener(IInputListener listener, int playerIndex)
        {
            logger.Debug("Adding {0} for player {1}.", listener.GetType().Name, playerIndex);

            listener.Attach(playerIndex);

            this._listeners.Add(listener);
        }
        /// <summary>
        /// Removes the listener from its player.
        /// </summary>
        /// <param name="listener">The listener.</param>
        public void RemoveListener(IInputListener listener)
        {
            logger.Debug("Removing {0} for player {1}.", listener.GetType().Name, listener.PlayerIndex);

            listener.Detach();

            this._listeners.Remove(listener);
        }
        /// <summary>
        /// Gets the listeners from the specified player index.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public IEnumerable<IInputListener> GetListeners(int playerIndex)
        {
            return this._listeners.Where(listener => listener.PlayerIndex == playerIndex);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Determines whether the player index is valid.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        private bool IsPlayerIndexValid(int playerIndex)
        {
            return playerIndex < this._inputs.Count;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the input event.
        /// </summary>
        /// <param name="stateEvent">The key event.</param>
        private void ProcessState(InputStateEvent stateEvent)
        {
            if (!this.IsPlayerIndexValid(stateEvent.PlayerIndex))
            {
                logger.Warn("Invalid input state event for player with id {0}.", stateEvent.PlayerIndex);
                return;
            }

            PlayerInput playerInput = this._inputs[stateEvent.PlayerIndex];
            playerInput[stateEvent.Id] = stateEvent.State;
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            var eventManager = XGL.Components.Get<IEventManager>();
            eventManager.Subscribe<InputStateEvent>(this.ProcessState);

            var gameLoop = XGL.Components.Get<IGameLoop>();
            gameLoop.Subscribe(this);
        }
        #endregion

        #region Implementation of IGameHandler
        /// <summary>
        /// Gets the index of the tick. Default: 0.
        /// </summary>
        public int TickIndex
        {
            get
            {
                //Ensure that the input manager will be
                //updated last, since we need to correctly push the
                //current input state the the previous one.
                return int.MaxValue;
            }
        }
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            foreach (PlayerInput playerInput in this._inputs)
            {
                playerInput.PushCurrentToPrevious();
            }
        }
        #endregion
    }
}
