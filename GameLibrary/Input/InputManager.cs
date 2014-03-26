using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Handlers;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input.Adapters;

namespace Xemio.GameLibrary.Input
{
    [Require(typeof(IGameLoop))]
    [Require(typeof(IEventManager))]

    public class InputManager : IConstructable, ISortableTickHandler
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        public InputManager()
        {
            this._inputs = new AutoProtectedList<PlayerInput>();
        }
        #endregion

        #region Fields
        private readonly IList<PlayerInput> _inputs; 
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
                if (playerIndex >= this._inputs.Count)
                    return null;

                return this._inputs[playerIndex];
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a player input.
        /// </summary>
        public PlayerInput CreatePlayerInput()
        {
            logger.Info("Creating player input with id {0}", this._inputs.Count);

            var playerInput = new PlayerInput(this, this._inputs.Count);
            this._inputs.Add(playerInput);

            return playerInput;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the input event.
        /// </summary>
        /// <param name="stateEvent">The key event.</param>
        private void ProcessState(InputStateEvent stateEvent)
        {
            if (stateEvent.Adapter.PlayerIndex >= this._inputs.Count)
            {
                logger.Warn("Invalid input state event for player with id {0}.", stateEvent.Adapter.PlayerIndex);
                stateEvent.Cancel();
            }
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            var eventManager = XGL.Components.Get<IEventManager>();
            var gameLoop = XGL.Components.Get<IGameLoop>();

            eventManager.Subscribe<InputStateEvent>(this.ProcessState);
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
