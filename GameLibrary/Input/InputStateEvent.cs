using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input.Adapters;

namespace Xemio.GameLibrary.Input
{
    public class InputStateEvent : ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputStateEvent" /> class.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        /// <param name="id">The id.</param>
        /// <param name="state">The new state.</param>
        public InputStateEvent(IInputAdapter adapter, string id, InputState state)
        {
            this.Id = id;
            this.State = state;
            this.Adapter = adapter;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public InputState State { get; private set; }
        /// <summary>
        /// Gets the adapter.
        /// </summary>
        public IInputAdapter Adapter { get; private set; }
        #endregion

        #region Implementation of ICancelableEvent
        /// <summary>
        /// Gets a value indicating whether the event propagation was canceled.
        /// </summary>
        public bool IsCanceled { get; private set; }
        /// <summary>
        /// Cancels the event propagation.
        /// </summary>
        public void Cancel()
        {
            this.IsCanceled = true;
        }
        #endregion
    }
}
