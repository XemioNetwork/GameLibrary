using Xemio.GameLibrary.Events;
using System.Windows.Forms;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

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
            Keys key = Keys.None;

            if (button == MouseButtons.Left) key = Keys.LeftMouse;
            if (button == MouseButtons.Right) key = Keys.RightMouse;
            if (button == MouseButtons.Middle) key = Keys.MouseWheel;

            return key;
        }
        /// <summary>
        /// Publishes the event.
        /// </summary>
        /// <param name="e">The event.</param>
        protected virtual void PublishEvent(IEvent e)
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

            var mouseEvent = new InputStateEvent(key, new InputState(true, 1.0f), this.PlayerIndex.Value);
            this.PublishEvent(mouseEvent);
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

            var mouseEvent = new InputStateEvent(key, new InputState(false, 0.0f), this.PlayerIndex.Value);
            this.PublishEvent(mouseEvent);
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
            Control surface = Control.FromHandle(XGL.Handle);

            surface.MouseMove += this.SurfaceMouseMove;
            surface.MouseDown += this.SurfaceMouseDown;
            surface.MouseUp += this.SurfaceMouseUp;
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public void OnDetached()
        {
            Control surface = Control.FromHandle(XGL.Handle);

            surface.MouseMove -= this.SurfaceMouseMove;
            surface.MouseDown -= this.SurfaceMouseDown;
            surface.MouseUp -= this.SurfaceMouseUp;
        }
        #endregion
    }
}
