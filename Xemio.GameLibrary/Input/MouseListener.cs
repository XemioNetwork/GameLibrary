using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using System.Windows.Forms;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Input
{
    public class MouseListener : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseListener"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public MouseListener(IntPtr handle)
        {
            Control surface = Control.FromHandle(handle);

            surface.MouseMove += SurfaceMouseMove;
            surface.MouseDown += SurfaceMouseDown;
            surface.MouseUp += SurfaceMouseUp;

            this._buttonStates = new Dictionary<MouseButtons, bool>();
        }
        #endregion

        #region Fields
        private Dictionary<MouseButtons, bool> _buttonStates;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified button is pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        public bool IsButtonPressed(MouseButtons button)
        {
            return this._buttonStates.ContainsKey(button) && this._buttonStates[button];
        }
        /// <summary>
        /// Determines whether the specified button is released.
        /// </summary>
        /// <param name="button">The button.</param>
        public bool IsButtonReleased(MouseButtons button)
        {
            return this._buttonStates.ContainsKey(button) && this._buttonStates[button];
        }
        /// <summary>
        /// Sets the state of the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void SetButtonState(MouseButtons button, bool state)
        {
            if (!this._buttonStates.ContainsKey(button))
            {
                this._buttonStates.Add(button, state);
            }

            this._buttonStates[button] = state;
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
            GraphicsDevice graphicsDevice = XGL.GetComponent<GraphicsDevice>();
            Vector2 divider = new Vector2(1, 1);

            if (graphicsDevice != null)
            {
                divider = graphicsDevice.Scale;
            }

            this.Position = new Vector2(e.X, e.Y) / divider;
        }
        /// <summary>
        /// Handles the MouseDown event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SurfaceMouseDown(object sender, MouseEventArgs e)
        {
            this.SetButtonState((MouseButtons)e.Button, true);
        }
        /// <summary>
        /// Handles the MouseUp event of the surface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SurfaceMouseUp(object sender, MouseEventArgs e)
        {
            this.SetButtonState((MouseButtons)e.Button, false);
        }
        #endregion
    }
}
