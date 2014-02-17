using System.Collections.Generic;
using System.Windows.Forms;
using NLog;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Input.Adapters
{
    public class MouseAdapter : BaseInputAdapter
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

            this.Send("mouse.position.x", new InputState(position.X));
            this.Send("mouse.position.y", new InputState(position.Y));
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
                this.Send(id, InputState.Released);
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
                this.Send(id, InputState.Released);
            }
        }
        #endregion
        
        #region Implementation of IInputAdapter
        /// <summary>
        /// Called when the input adapter was attached to the player.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public override void Attach(int playerIndex)
        {
            base.Attach(playerIndex);

            var surface = XGL.Components.Require<WindowSurface>();
            surface.Control.MouseMove += this.OnMouseMove;
            surface.Control.MouseDown += this.OnMouseDown;
            surface.Control.MouseUp += this.OnMouseUp;
        }
        /// <summary>
        /// Called when the input adapter was detached from the player.
        /// </summary>
        public override void Detach()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            surface.Control.MouseMove -= this.OnMouseMove;
            surface.Control.MouseDown -= this.OnMouseDown;
            surface.Control.MouseUp -= this.OnMouseUp;

            base.Detach();
        }
        #endregion
    }
}
