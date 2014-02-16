using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input
{
    public class PlayerInput
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInput" /> class.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public PlayerInput(int playerIndex)
        {
            this.PlayerIndex = playerIndex;

            this._states = new Dictionary<string, InputState>();
            this._previousStates = new Dictionary<string, InputState>();

            this._bindings = new Dictionary<string, IList<string>>();
        }
        #endregion
        
        #region Fields
        private readonly Dictionary<string, InputState> _states;
        private readonly Dictionary<string, InputState> _previousStates;

        private readonly Dictionary<string, IList<string>> _bindings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; } 
        /// <summary>
        /// Gets the <see cref="Xemio.GameLibrary.Input.InputState"/> with the specified id.
        /// </summary>
        public InputState this[string id]
        {
            get
            {
                if (!this._states.ContainsKey(id))
                {
                    return InputState.Released;
                }

                return this._states[id];
            }
            set
            {
                this._states[id] = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the reference.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public InputReference Reference(string id)
        {
            return new InputReference(XGL.Components.Get<InputManager>(), this.PlayerIndex, id);
        }
        /// <summary>
        /// Gets the current active state for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public bool IsActive(string id)
        {
            return this._states.ContainsKey(id) && this._states[id].Active;
        }
        /// <summary>
        /// Gets the last active state for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public bool WasPreviouslyActive(string id)
        {
            return this._previousStates.ContainsKey(id) && this._previousStates[id].Active;
        }
        /// <summary>
        /// Determines whether the specified id has a state.
        /// </summary>
        /// <param name="id">The id.</param>
        public bool Contains(string id)
        {
            return this.HasBinding(id) || this.HasState(id);
        }
        /// <summary>
        /// Determines whether the specified identifier is contained inside the states dictionary.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public bool HasState(string id)
        {
            return this._states.ContainsKey(id);
        }
        /// <summary>
        /// Determines whether the specified binding identifier is contained.
        /// </summary>
        /// <param name="bindingId">The binding identifier.</param>
        public bool HasBinding(string bindingId)
        {
            return this._bindings.ContainsKey(bindingId);
        }
        /// <summary>
        /// Binds the specified source to the target key id.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public void Bind(string source, string target)
        {
            if (!this._bindings.ContainsKey(target))
            {
                this._bindings[target] = new List<string>();
            }

            this._bindings[target].Add(source);
        }
        /// <summary>
        /// Unbinds the specified source.
        /// </summary>
        /// <param name="bindingId">The binding identifier.</param>
        public void Unbind(string bindingId)
        {
            this._bindings.Remove(bindingId);
        }
        /// <summary>
        /// Resolves the specified binding and returns its original keys.
        /// </summary>
        /// <param name="bindingId">The binding identifier.</param>
        public IEnumerable<string> Resolve(string bindingId)
        {
            if (this._bindings.ContainsKey(bindingId))
            {
                var resolvedIds = new List<string>();
                
                if (this.HasState(bindingId))
                {
                    resolvedIds.Add(bindingId);
                }

                foreach (string id in this._bindings[bindingId])
                {
                    if (this.HasBinding(id))
                    {
                        resolvedIds.AddRange(this.Resolve(id));
                    }
                    else
                    {
                        resolvedIds.Add(id);
                    }
                }

                return resolvedIds;
            }

            return new[] {bindingId};
        }
        /// <summary>
        /// Gets the last state of the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public InputState GetPreviousState(string id)
        {
            if (!this._previousStates.ContainsKey(id))
            {
                return InputState.Released;
            }

            return this._previousStates[id];
        }
        /// <summary>
        /// Gets the inputs matching the given filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public IEnumerable<string> GetKeys(Func<string, bool> filter)
        {
            return this._states.Keys.Where(filter);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Updates the changes from the current into the last state.
        /// </summary>
        internal void PushCurrentToPrevious()
        {
            this._previousStates.Clear();
            foreach (KeyValuePair<string, InputState> pair in this._states)
            {
                this._previousStates[pair.Key] = pair.Value;
            }
        }
        #endregion
    }
}
