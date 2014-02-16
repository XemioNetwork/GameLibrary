using System.Collections;
using System.Collections.Generic;
using NLog;
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
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion
        
        #region Methods
        /// <summary>
        /// Converts mouse buttons into our keys enum.
        /// </summary>
        /// <param name="button">The button.</param>
        private IEnumerable<string> GetKeys(MouseButtons button)
        {
            if (button.HasFlag(MouseButtons.Left)) 
                yield return "mouse.left";

            if (button.HasFlag(MouseButtons.Right))
                yield return "mouse.right";

            if (button.HasFlag(MouseButtons.Middle))
                yield return "mouse.wheel";
        }
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
        /// Handles the MouseMove event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            var divider = new Vector2(1, 1);

            if (graphicsDevice != null)
            {
                divider = graphicsDevice.Scale;
            }

            Vector2 position = new Vector2(e.X, e.Y) / divider;

            this.PublishEvent(new InputStateEvent("mouse.position.x", new InputState(position.X), this.PlayerIndex));
            this.PublishEvent(new InputStateEvent("mouse.position.y", new InputState(position.Y), this.PlayerIndex));
        }
        /// <summary>
        /// Handles the MouseDown event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            foreach (string id in this.GetKeys(e.Button))
            {
                this.PublishEvent(new InputStateEvent(id, InputState.Released, this.PlayerIndex));
            }
        }
        /// <summary>
        /// Handles the MouseUp event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            foreach (string id in this.GetKeys(e.Button))
            {
                this.PublishEvent(new InputStateEvent(id, InputState.Released, this.PlayerIndex));
            }
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
            surface.Control.MouseMove += this.OnMouseMove;
            surface.Control.MouseDown += this.OnMouseDown;
            surface.Control.MouseUp += this.OnMouseUp;
        }
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        public void Detach()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            surface.Control.MouseMove -= this.OnMouseMove;
            surface.Control.MouseDown -= this.OnMouseDown;
            surface.Control.MouseUp -= this.OnMouseUp;
        }
        #endregion
    }
}
