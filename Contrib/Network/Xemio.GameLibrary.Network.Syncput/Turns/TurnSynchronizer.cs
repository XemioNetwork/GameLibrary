using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;

namespace Xemio.GameLibrary.Network.Syncput.Turns
{
    public class TurnSynchronizer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnSynchronizer"/> class.
        /// </summary>
        /// <param name="syncput">The syncput.</param>
        public TurnSynchronizer(Syncput syncput)
        {
            this.TurnLength = 1;

            this._processedTurnIndex = -1;
            this._sender = new TurnSender();
            this._syncput = syncput;
        }
        #endregion

        #region Fields
        private int _tickCount;
        private int _processedTurnIndex;

        private readonly TurnSender _sender;
        private readonly Syncput _syncput;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the turn sequence.
        /// </summary>
        public int TurnSequence { get; private set; }
        /// <summary>
        /// Gets or sets the length of a turn.
        /// </summary>
        public int TurnLength { get; set; }
        /// <summary>
        /// Gets a value indicating whether a simulation is requested.
        /// </summary>
        public bool SimulationRequested { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is locked.
        /// </summary>
        public bool IsLocked
        {
            get
            {
                InputManager inputManager = XGL.Components.Get<InputManager>();
                bool isLocked = false;

                for (int i = 0; i < inputManager.PlayerInputs.Count; i++)
                {
                    if (inputManager.GetListeners(i)
                                    .OfType<RemoteListener>()
                                    .Any(r => !r.CanSimulate(this.TurnSequence)))
                    {
                        isLocked = true;
                        break;
                    }
                }

                return isLocked;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Processes the specified turn.
        /// </summary>
        private void ProcessCurrentTurn()
        {
            if (this._processedTurnIndex >= this.TurnSequence)
                return;

            this._processedTurnIndex++;

            if (XGL.Components.Get<IClient>() != null)
            {
                this._sender.SendTurn(this.TurnSequence + 1);
            }
        }
        /// <summary>
        /// Awaits the synchronization.
        /// </summary>
        private void AwaitSynchronization()
        {
            while (this.IsLocked)
            {
                Application.DoEvents();
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (!this._syncput.IsGameStarted())
                return;

            if (!this.SimulationRequested)
            {
                this._tickCount++;
                if (this._tickCount > this.TurnLength)
                {
                    this._tickCount = 0;

                    this.TurnSequence++;
                    this.SimulationRequested = true;
                }
            }

            this.ProcessCurrentTurn();
            this.AwaitSynchronization();
            
            if (this.SimulationRequested)
            {
                InputManager inputManager = XGL.Components.Get<InputManager>();
                for (int i = 0; i < inputManager.PlayerInputs.Count; i++)
                {
                    foreach (RemoteListener listener in inputManager
                        .GetListeners(i)
                        .OfType<RemoteListener>())
                    {
                        listener.Simulate(this.TurnSequence);
                    }
                }

                this.SimulationRequested = false;
            }
        }
        #endregion
    }
}
