using System.Windows.Forms;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Input.Keyboard
{
    public class KeyboardListener : IInputListener
    {
        #region Properties
        /// <summary>
        /// Gets the event manager.
        /// </summary>
        protected EventManager EventManager
        {
            get { return XGL.Components.Get<EventManager>(); }
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
            this.EventManager.Publish(new KeyEvent((Keys)e.KeyCode, new InputState(true, 1.0f), this.PlayerIndex.Value));
        }
        /// <summary>
        /// Handles the KeyUp event of the surface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SurfaceKeyUp(object sender, KeyEventArgs e)
        {
            this.EventManager.Publish(new KeyEvent((Keys)e.KeyCode, new InputState(false, 0.0f), this.PlayerIndex.Value));
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
            var graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            var surface = Control.FromHandle(graphicsDevice.Handle);

            surface.KeyDown += this.SurfaceKeyDown;
            surface.KeyUp += this.SurfaceKeyUp;
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public void OnDetached()
        {
            var graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            var surface = Control.FromHandle(graphicsDevice.Handle);

            surface.KeyDown -= this.SurfaceKeyDown;
            surface.KeyUp -= this.SurfaceKeyUp;
        }
        #endregion
    }
}
