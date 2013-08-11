using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input
{
    public class PlayerInput
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInput"/> class.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public PlayerInput(int playerIndex)
        {
            this.PlayerIndex = playerIndex;

            this._states = new Dictionary<Keys, InputState>();
            this._lastStates = new Dictionary<Keys, InputState>();
        }
        #endregion
        
        #region Fields
        private readonly Dictionary<Keys, InputState> _states;
        private readonly Dictionary<Keys, InputState> _lastStates;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        /// <summary>
        /// Gets or sets the mouse position.
        /// </summary>
        public Vector2 MousePosition { get; internal set; }
        /// <summary>
        /// Gets the <see cref="Xemio.GameLibrary.Input.InputState"/> with the specified key.
        /// </summary>
        public InputState this[Keys key]
        {
            get
            {
                if (!this._states.ContainsKey(key))
                {
                    return InputState.Empty;
                }

                return this._states[key];
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the current active state for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private bool GetValue(Keys key)
        {
            return this._states.ContainsKey(key) && this._states[key].Active;
        }
        /// <summary>
        /// Gets the last active state for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private bool GetLastValue(Keys key)
        {
            return this._lastStates.ContainsKey(key) && this._lastStates[key].Active;
        }
        /// <summary>
        /// Determines whether the specified key has a state.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool HasState(Keys key)
        {
            return this._states.ContainsKey(key);
        }
        /// <summary>
        /// Determines whether the specified key is down.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyDown(Keys key)
        {
            return this.GetValue(key);
        }
        /// <summary>
        /// Determines whether the specified key is up.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyUp(Keys key)
        {
            return !this.GetValue(key);
        }
        /// <summary>
        /// Determines whether the specified key is first down inside the current frame.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyPressed(Keys key)
        {
            return this.GetValue(key) && !this.GetLastValue(key);
        }
        /// <summary>
        /// Determines whether the specified key is first up inside the current frame.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyReleased(Keys key)
        {
            return !this.GetValue(key) && this.GetLastValue(key);
        }
        /// <summary>
        /// Sets the state of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The state.</param>
        public void SetState(Keys key, InputState state)
        {
            if (!this._states.ContainsKey(key))
            {
                this._states.Add(key, state);
            }

            this._states[key] = state;
        }
        /// <summary>
        /// Gets the last state of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public InputState GetLastState(Keys key)
        {
            if (!this._lastStates.ContainsKey(key))
            {
                return InputState.Empty;
            }

            return this._lastStates[key];
        }
        /// <summary>
        /// Gets the inputs matching the given filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public IEnumerable<Keys> GetKeys(Func<Keys, bool> filter)
        {
            return Enum.GetValues(typeof(Keys)).Cast<Keys>().Where(filter);
        }
        /// <summary>
        /// Gets the pressed inputs.
        /// </summary>
        public IEnumerable<Keys> GetPressedKeys()
        {
            return this.GetKeys(this.IsKeyPressed);
        }
        /// <summary>
        /// Gets the released inputs.
        /// </summary>
        public IEnumerable<Keys> GetReleasedKeys()
        {
            return this.GetKeys(this.IsKeyReleased);
        }
        /// <summary>
        /// Updates the changes from the current into the last state.
        /// </summary>
        public void UpdateStates()
        {
            this._lastStates.Clear();
            foreach (KeyValuePair<Keys, InputState> pair in this._states)
            {
                this._lastStates.Add(pair.Key, pair.Value);
            }
        }
        #endregion
    }
}
