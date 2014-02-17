using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input.Adapters;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input
{
    public class PlayerInput
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInput" /> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        /// <param name="playerIndex">Index of the player.</param>
        public PlayerInput(InputManager inputManager, int playerIndex)
        {
            this.PlayerIndex = playerIndex;

            this._inputManager = inputManager;

            this._states = new Dictionary<string, InputState>();
            this._previousStates = new Dictionary<string, InputState>();

            this._adapters = new List<IInputAdapter>();
            this._bindings = new Dictionary<string, IList<string>>();

            var eventManager = XGL.Components.Get<IEventManager>();
            eventManager.Subscribe(EventFilter<InputStateEvent>
                .For(this.ProcessInputEvent)
                .WithCondition(evt => this._adapters.Contains(evt.Adapter))
                .Create());
        }
        #endregion
        
        #region Fields
        private readonly InputManager _inputManager;

        private readonly Dictionary<string, InputState> _states;
        private readonly Dictionary<string, InputState> _previousStates;

        private readonly List<IInputAdapter> _adapters; 
        private readonly Dictionary<string, IList<string>> _bindings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        /// <summary>
        /// Gets the adapters.
        /// </summary>
        public IEnumerable<IInputAdapter> Adapters
        {
            get { return this._adapters; }
        } 
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

        #region Private Methods
        /// <summary>
        /// Determines whether the specified identifier is contained inside the states dictionary.
        /// </summary>
        /// <param name="id">The identifier.</param>
        private bool HasState(string id)
        {
            return this._states.ContainsKey(id);
        }
        /// <summary>
        /// Determines whether the specified binding identifier is contained.
        /// </summary>
        /// <param name="bindingId">The binding identifier.</param>
        private bool HasBinding(string bindingId)
        {
            return this._bindings.ContainsKey(bindingId);
        }
        /// <summary>
        /// Resolves the binding ids.
        /// </summary>
        /// <param name="id">The binding identifier.</param>
        private IEnumerable<string> ResolveIds(string id)
        {
            if (this._bindings.ContainsKey(id))
            {
                var resolvedIds = new List<string>();

                if (this.HasState(id))
                {
                    resolvedIds.Add(id);
                }

                foreach (string resolvedId in this._bindings[id])
                {
                    if (this.HasBinding(resolvedId))
                    {
                        resolvedIds.AddRange(this.ResolveIds(resolvedId));
                    }
                    else
                    {
                        resolvedIds.Add(resolvedId);
                    }
                }

                return resolvedIds;
            }

            return new[] { id };
        }
        /// <summary>
        /// Processes the input event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void ProcessInputEvent(InputStateEvent evt)
        {
            this[evt.Id] = evt.State;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the reference.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public InputReference Get(string id)
        {
            return new InputReference(this._inputManager, this.PlayerIndex, id, this.ResolveIds(id));
        }
        /// <summary>
        /// Attaches the specified adapter.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        public void Attach(IInputAdapter adapter)
        {
            adapter.Attach(this.PlayerIndex);
            this._adapters.Add(adapter);
        }
        /// <summary>
        /// Detaches the specified adapter.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        public void Detach(IInputAdapter adapter)
        {
            adapter.Detach();
            this._adapters.Remove(adapter);
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
        /// Gets the inputs matching the given filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public IEnumerable<string> GetIds(Func<string, bool> filter)
        {
            return this._states.Keys.Where(filter);
        }
        /// <summary>
        /// Determines whether this instance has the specified adapter.
        /// </summary>
        public bool HasAdapter<T>() where T : IInputAdapter
        {
            return this.Adapters.Any(adapter => adapter is T);
        }
        /// <summary>
        /// Gets an adapter by a specified type.
        /// </summary>
        public T GetAdapter<T>() where T : IInputAdapter
        {
            return (T)this.Adapters.FirstOrDefault(adapter => adapter is T);
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
        /// <summary>
        /// Gets the current active state for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        internal bool IsActive(string id)
        {
            return this._states.ContainsKey(id) && this._states[id].Active;
        }
        /// <summary>
        /// Gets the last active state for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        internal bool WasPreviouslyActive(string id)
        {
            return this._previousStates.ContainsKey(id) && this._previousStates[id].Active;
        }
        #endregion
    }
}
