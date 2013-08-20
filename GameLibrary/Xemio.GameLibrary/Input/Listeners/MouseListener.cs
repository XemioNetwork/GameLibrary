using Xemio.GameLibrary.Events;
using System.Windows.Forms;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Input.Listeners
{
    public class MouseListener : IInputListener
    {
        #region Methods
        /// <summary>
        /// Converts mouse buttons into our keys enum.
        /// </summary>
        /// <param name="button">The button.</param>
        private Keys GetKeys(MouseButtons button)
        {
            Keys keys = Keys.None;

            if (button.HasFlag(MouseButtons.Left)) keys |= Keys.LeftMouse;
            if (button.HasFlag(MouseButtons.Right)) keys |= Keys.RightMouse;
            if (button.HasFlag(MouseButtons.Middle)) keys |= Keys.MouseWheel;

            return keys;
        }
        /// <summary>
        /// Publishes the event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="e">The event.</param>
        protected virtual void PublishEvent<TEvent>(TEvent e) where TEvent : IEvent
        {
            var eventManager = XGL.Components.Get<EventManager>();
            eventManager.Publish(e);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the MouseMove event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SurfaceMouseMove(object sender, MouseEventArgs e)
        {
            GraphicsDevice graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            Vector2 divider = new Vector2(1, 1);

            if (graphicsDevice != null)
            {
                divider = graphicsDevice.Scale;
            }

            Vector2 position = new Vector2(e.X, e.Y) / divider;
            this.PublishEvent(new MousePositionEvent(position, this.PlayerIndex.Value));
        }
        /// <summary>
        /// Handles the MouseDown event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SurfaceMouseDown(object sender, MouseEventArgs e)
        {
            Keys key = this.GetKeys(e.Button);

            if (key == Keys.None)
                return;

            this.PublishEvent(new InputStateEvent(key, new InputState(true, 1.0f), this.PlayerIndex.Value));
        }
        /// <summary>
        /// Handles the MouseUp event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SurfaceMouseUp(object sender, MouseEventArgs e)
        {
            Keys key = this.GetKeys(e.Button);

            if (key == Keys.None)
                return;

            this.PublishEvent(new InputStateEvent(key, new InputState(false, 0.0f), this.PlayerIndex.Value));
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

            surface.Control.MouseMove += this.SurfaceMouseMove;
            surface.Control.MouseDown += this.SurfaceMouseDown;
            surface.Control.MouseUp += this.SurfaceMouseUp;
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public void OnDetached()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            surface.Control.MouseMove -= this.SurfaceMouseMove;
            surface.Control.MouseDown -= this.SurfaceMouseDown;
            surface.Control.MouseUp -= this.SurfaceMouseUp;
        }
        #endregion
    }
}
