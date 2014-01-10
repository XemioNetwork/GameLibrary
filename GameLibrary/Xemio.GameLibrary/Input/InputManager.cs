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
    [Require(typeof(EventManager))]

    public class InputManager : IConstructable, IGameHandler
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly IList<IInputListener> _listeners;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the local player input.
        /// </summary>
        public PlayerInput LocalInput
        {
            get { return this.PlayerInputs.First(); }
        }
        /// <summary>
        /// Gets the player inputs.
        /// </summary>
        public IList<PlayerInput> PlayerInputs { get; private set; }
        /// <summary>
        /// Gets the <see cref="Xemio.GameLibrary.Input.PlayerInput"/> with the specified player index.
        /// </summary>
        public PlayerInput this[int playerIndex]
        {
            get
            {
                if (!this.IsPlayerIndexValid(playerIndex))
                    return null;

                return this.PlayerInputs[playerIndex];
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
            this.PlayerInputs = new List<PlayerInput>();
        }
        #endregion Constructors
        
        #region Methods
        /// <summary>
        /// Creates a player input.
        /// </summary>
        public PlayerInput CreateInput()
        {
            logger.Info("Creating player input with id {0}", this.PlayerInputs.Count);

            var playerInput = new PlayerInput(this.PlayerInputs.Count);
            this.PlayerInputs.Add(playerInput);

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

            listener.PlayerIndex = playerIndex;
            listener.OnAttached();

            this._listeners.Add(listener);
        }
        /// <summary>
        /// Removes the listener from its player.
        /// </summary>
        /// <param name="listener">The listener.</param>
        public void RemoveListener(IInputListener listener)
        {
            logger.Debug("Removing {0} for player {1}.", listener.GetType().Name, listener.PlayerIndex);

            listener.PlayerIndex = null;
            listener.OnDetached();

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
            return playerIndex < this.PlayerInputs.Count;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the input event.
        /// </summary>
        /// <param name="stateEvent">The key event.</param>
        private void HandleInputEvent(KeyStateEvent stateEvent)
        {
            if (!this.IsPlayerIndexValid(stateEvent.PlayerIndex))
            {
                logger.Debug("Invalid input state event for player with id {0}.", stateEvent.PlayerIndex);
                return;
            }

            PlayerInput playerInput = this.PlayerInputs[stateEvent.PlayerIndex];
            playerInput.SetState(stateEvent.Key, stateEvent.State);
        }
        /// <summary>
        /// Handles a mouse position event.
        /// </summary>
        /// <param name="mouseEvent">The mouse event.</param>
        private void HandleMousePositionEvent(MousePositionEvent mouseEvent)
        {
            if (!this.IsPlayerIndexValid(mouseEvent.PlayerIndex))
            {
                logger.Debug("Invalid mouse position event for player with id {0}.", mouseEvent.PlayerIndex);
                return;
            }

            PlayerInput playerInput = this.PlayerInputs[mouseEvent.PlayerIndex];
            playerInput.MousePosition = mouseEvent.Position;
        }
        #endregion Event Handlers

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            var eventManager = XGL.Components.Get<EventManager>();

            eventManager.Subscribe<KeyStateEvent>(this.HandleInputEvent);
            eventManager.Subscribe<MousePositionEvent>(this.HandleMousePositionEvent);

            var gameLoop = XGL.Components.Get<IGameLoop>();
            gameLoop.Subscribe(this);
        }
        #endregion

        #region Implementation of IGameHandler
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            foreach (PlayerInput playerInput in this.PlayerInputs)
            {
                playerInput.UpdateStates();
            }
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public void Render()
        {
        }
        #endregion
    }
}
