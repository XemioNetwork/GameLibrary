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
        private void SurfaceKeyDown(object sender, KeyEventArgs e)
        {
            this.PublishEvent(new KeyStateEvent((Keys)e.KeyCode, new InputState(true, 1.0f), this.PlayerIndex.Value));
        }
        /// <summary>
        /// Handles the KeyUp event of the surface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SurfaceKeyUp(object sender, KeyEventArgs e)
        {
            this.PublishEvent(new KeyStateEvent((Keys)e.KeyCode, new InputState(false, 0.0f), this.PlayerIndex.Value));
        }
        #endregion

        #region Implementation of IInputListener
        /// <summary>
        /// Gets or sets the index of the player.
        /// </summary>
        public int? PlayerIndex { get; set; }
        /// <summary>
        /// Called when the input listener was attached to the player.
        /// </summary>
        public void OnAttached()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            surface.Control.KeyDown += this.SurfaceKeyDown;
            surface.Control.KeyUp += this.SurfaceKeyUp;

            logger.Debug("Attached keyboard listener {0}.", this.PlayerIndex.Value);
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public void OnDetached()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            surface.Control.KeyDown -= this.SurfaceKeyDown;
            surface.Control.KeyUp -= this.SurfaceKeyUp;

            logger.Debug("Detached keyboard listener {0}.", this.PlayerIndex.Value);
        }
        #endregion
    }
}
