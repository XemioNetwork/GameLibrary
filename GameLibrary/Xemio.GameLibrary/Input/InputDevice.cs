using System;
using System.Linq;
using System.Collections.Generic;

namespace Xemio.GameLibrary.Input
{
    public class InputDevice<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputDevice&lt;T&gt;"/> class.
        /// </summary>
        public InputDevice()
        {
            this._states = new Dictionary<T, InputState>();
            this._lastStates = new Dictionary<T, InputState>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<T, InputState> _states;
        private readonly Dictionary<T, InputState> _lastStates;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Xemio.GameLibrary.Input.InputState"/> with the specified key.
        /// </summary>
        public InputState this[T key]
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
        private bool GetValue(T key)
        {
            return this._states.ContainsKey(key) && this._states[key].Active;
        }
        /// <summary>
        /// Gets the last active state for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private bool GetLastValue(T key)
        {
            return this._lastStates.ContainsKey(key) && this._lastStates[key].Active;
        }
        /// <summary>
        /// Determines whether the specified key is down.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyDown(T key)
        {
            return this.GetValue(key);
        }
        /// <summary>
        /// Determines whether the specified key is up.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyUp(T key)
        {
            return !this.GetValue(key);
        }
        /// <summary>
        /// Determines whether the specified key is first down inside the current frame.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyPressed(T key)
        {
            return this.GetValue(key) && !this.GetLastValue(key);
        }
        /// <summary>
        /// Determines whether the specified key is first up inside the current frame.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyReleased(T key)
        {
            return !this.GetValue(key) && this.GetLastValue(key);
        }
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
        /// Gets the inputs matching the given filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public IEnumerable<T> GetKeys(Func<KeyValuePair<T, InputState>, bool> filter)
        {
            return this._states.Where(filter).Select(f => f.Key);
        }
        /// <summary>
        /// Gets the pressed inputs.
        /// </summary>
        public IEnumerable<T> GetPressedKeys()
        {
            return this.GetKeys(f => this.IsKeyPressed(f.Key) && this[f.Key].Active);
        }
        /// <summary>
        /// Gets the released inputs.
        /// </summary>
        public IEnumerable<T> GetReleasedKeys()
        {
            return this.GetKeys(f => this.IsKeyReleased(f.Key) && this[f.Key].Active == false);
        }
        /// <summary>
        /// Updates the changes from the current into the last state.
        /// </summary>
        public void UpdateStates()
        {
            this._lastStates.Clear();
            foreach (KeyValuePair<T, InputState> pair in this._states)
            {
                this._lastStates.Add(pair.Key, pair.Value);
            }
        }
        #endregion
    }
}