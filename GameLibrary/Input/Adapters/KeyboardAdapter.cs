using System.Windows.Forms;
using NLog;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Input.Adapters
{
    public class KeyboardAdapter : BaseInputAdapter
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
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

            this.Send(id, InputState.Pressed);
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

            this.Send(id, InputState.Released);
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
            surface.Control.KeyDown += this.OnKeyDown;
            surface.Control.KeyUp += this.OnKeyUp;
        }
        /// <summary>
        /// Called when the input adapter was detached from the player.
        /// </summary>
        public override void Detach()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            surface.Control.KeyDown -= this.OnKeyDown;
            surface.Control.KeyUp -= this.OnKeyUp;

            base.Detach();
        }
        #endregion
    }
}
