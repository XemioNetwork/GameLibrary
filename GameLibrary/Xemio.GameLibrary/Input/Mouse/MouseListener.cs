using System.Collections.Generic;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
using System.Windows.Forms;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Input.Mouse
{
    public class MouseListener : IInputListener
    {
        #region Properties
        /// <summary>
        /// Gets the event manager.
        /// </summary>
        protected EventManager EventManager
        {
            get { return XGL.Components.Get<EventManager>(); }
        }
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
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
            
            this.Position = new Vector2(e.X, e.Y) / divider;
            this.EventManager.Publish(new MouseMoveEvent(this.Position, this.PlayerIndex.Value));
        }
        /// <summary>
        /// Handles the MouseDown event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SurfaceMouseDown(object sender, MouseEventArgs e)
        {
            var mouseEvent = new MouseEvent((MouseButtons)e.Button, new InputState(true, 1.0f), this.PlayerIndex.Value);
            this.EventManager.Publish(mouseEvent);
        }
        /// <summary>
        /// Handles the MouseUp event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SurfaceMouseUp(object sender, MouseEventArgs e)
        {
            var mouseEvent = new MouseEvent((MouseButtons)e.Button, new InputState(false, 0.0f), this.PlayerIndex.Value);
            this.EventManager.Publish(mouseEvent);
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

            Control surface = Control.FromHandle(graphicsDevice.Handle);

            surface.MouseMove += this.SurfaceMouseMove;
            surface.MouseDown += this.SurfaceMouseDown;
            surface.MouseUp += this.SurfaceMouseUp;
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public void OnDetached()
        {
            var graphicsDevice = XGL.Components.Get<GraphicsDevice>();

            Control surface = Control.FromHandle(graphicsDevice.Handle);

            surface.MouseMove -= this.SurfaceMouseMove;
            surface.MouseDown -= this.SurfaceMouseDown;
            surface.MouseUp -= this.SurfaceMouseUp;
        }
        #endregion
    }
}
