using System.Windows.Forms;
using NLog;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Input.Listeners
{
    public class KeyboardListener : IInputListener
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion
        
        #region Methods
        /// <summary>
        /// Publishes the event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="e">The event.</param>
        protected virtual void PublishEvent<TEvent>(TEvent e) where TEvent : class, IEvent
        {
            var eventManager = XGL.Components.Get<IEventManager>();
            eventManager.Publish(e);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the KeyDown event of the surface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            string keyName = e.KeyCode.ToString();
            string id = "key." + keyName.ToLower();

            this.PublishEvent(new InputStateEvent(id, InputState.Pressed, this.PlayerIndex));
        }
        /// <summary>
        /// Handles the KeyUp event of the surface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            string keyName = e.KeyCode.ToString();
            string id = "key." + keyName.ToLower();

            this.PublishEvent(new InputStateEvent(id, InputState.Released, this.PlayerIndex));
        }
        #endregion

        #region Implementation of IInputListener
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        /// <summary>
        /// Called when the input listener was attached to the player.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public void Attach(int playerIndex)
        {
            this.PlayerIndex = playerIndex;

            var surface = XGL.Components.Require<WindowSurface>();
            surface.Control.KeyDown += this.OnKeyDown;
            surface.Control.KeyUp += this.OnKeyUp;
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public void Detach()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            surface.Control.KeyDown -= this.OnKeyDown;
            surface.Control.KeyUp -= this.OnKeyUp;
        }
        #endregion
    }
}
