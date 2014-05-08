using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Handles;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Subscribers;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input.Adapters;
using Xemio.GameLibrary.Logging;

namespace Xemio.GameLibrary.Input
{
    [Require(typeof(IGameLoop))]
    [Require(typeof(IEventManager))]

    public class InputManager : IConstructable, ISortableTickSubscriber, IHandle<InputStateEvent>
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
        
        #region Implementation of IHandle<in InputStateEvent>
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        public void Handle(InputStateEvent evt)
        {
            if (evt.Adapter.PlayerIndex >= this._inputs.Count)
            {
                logger.Warn("Invalid input state event for player with id {0}.", evt.Adapter.PlayerIndex);
                evt.Cancel();
            }
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
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
