using System;
using System.Linq;
using System.Collections.Generic;

namespace Xemio.GameLibrary.Input
{
    public class InputDevice<T>
    {
        #region Fields
        private readonly Dictionary<T, InputState> _states = new Dictionary<T, InputState>();
        private readonly Dictionary<T, InputState> _lastStates = new Dictionary<T, InputState>();
        #endregion Fields

        #region Methods
        /// <summary>
        /// Sets the state of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The state.</param>
        public void SetState(T key, InputState state)
        {
            if (!this._states.ContainsKey(key))
            {
                this._states.Add(key, state);
            }

            this._states[key] = state;
        }
        /// <summary>
        /// Gets the state of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public InputState GetState(T key)
        {
            if (!this._states.ContainsKey(key))
            {
                return InputState.Empty;
            }

            return this._states[key];
        }
        /// <summary>
        /// Gets the last state of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public InputState GetLastState(T key)
        {
            if (!this._lastStates.ContainsKey(key))
            {
                return InputState.Empty;
            }

            return this._lastStates[key];
        }
        /// <summary>
        /// Returns whether the state of the specified key has changed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool StateChanged(T key)
        {
            //First keypress
            if (this._lastStates.ContainsKey(key) == false &&
                this._states.ContainsKey(key))
            {
                return true;
            }

            //Not pressed yet
            if (this._states.ContainsKey(key) == false)
            {
                return false;
            }

            return this._states[key].Active != this._lastStates[key].Active;
        }

        /// <summary>
        /// Gets the inputs matching the given filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public IEnumerable<T> GetInputs(Func<KeyValuePair<T, InputState>, bool> filter)
        {
            return this._states
                .Where(filter)
                .Select(f => f.Key);
        }
        /// <summary>
        /// Gets the pressed inputs.
        /// </summary>
        public IEnumerable<T> GetPressedInputs()
        {
            return this.GetInputs(f => this.StateChanged(f.Key) && this.GetState(f.Key).Active);
        }
        /// <summary>
        /// Gets the released inputs.
        /// </summary>
        public IEnumerable<T> GetReleasedInputs()
        {
            return this.GetInputs(f => this.StateChanged(f.Key) && this.GetState(f.Key).Active == false);
        }

        /// <summary>
        /// Updates the changes from the current into the last state.
        /// </summary>
        public void UpdateChanges()
        {
            this._lastStates.Clear();
            foreach (KeyValuePair<T, InputState> pair in this._states)
            {
                this._lastStates.Add(pair.Key, pair.Value);
            }
        }
        #endregion Methods
    }
}