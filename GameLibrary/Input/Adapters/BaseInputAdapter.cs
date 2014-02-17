using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Input.Adapters
{
    public class BaseInputAdapter : IInputAdapter
    {
        #region Methods
        /// <summary>
        /// Sends the specified input state to the currently attached player.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="state">The state.</param>
        protected virtual void Send(string id, InputState state)
        {
            var eventManager = XGL.Components.Get<IEventManager>();
            eventManager.Publish(new InputStateEvent(this, id, state));
        }
        #endregion

        #region Implementation of IInputAdapter
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; protected set; }
        /// <summary>
        /// Called when the input listener was attached to the player.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public virtual void Attach(int playerIndex)
        {
            this.PlayerIndex = playerIndex;
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public virtual void Detach()
        {
            this.PlayerIndex = -1;
        }
        #endregion
    }
}
