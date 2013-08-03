using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using System.Windows.Forms;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Input.Events.Keyboard;

namespace Xemio.GameLibrary.Input
{
    public class KeyListener : IGameHandler, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyListener"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public KeyListener(IntPtr handle)
        {
            var surface = Control.FromHandle(handle);

            surface.KeyDown += this.SurfaceKeyDown;
            surface.KeyUp += this.SurfaceKeyUp;

            this._keyStates = new Dictionary<Keys, bool>();
            this._lastStates = new Dictionary<Keys, bool>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Keys, bool> _keyStates;
        private readonly Dictionary<Keys, bool> _lastStates;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the event manager.
        /// </summary>
        protected EventManager EventManager
        {
            get { return XGL.Components.Get<EventManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the state of the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void SetKeyState(Keys key, bool state)
        {
            if (!this._keyStates.ContainsKey(key))
            {
                this._keyStates.Add(key, state);
            }

            this._keyStates[key] = state;
        }
        /// <summary>
        /// Updates the states.
        /// </summary>
        private void UpdateStates()
        {
            this._lastStates.Clear();
            foreach (KeyValuePair<Keys, bool> pair in this._keyStates)
            {
                this._lastStates.Add(pair.Key, pair.Value);
            }
        }
        /// <summary>
        /// Determines whether the specified key is held.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyDown(Keys key)
        {
            return this._keyStates.ContainsKey(key) && this._keyStates[key];
        }
        /// <summary>
        /// Determines whether the specified key is not held.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyUp(Keys key)
        {
            return !this._keyStates.ContainsKey(key) || !this._keyStates[key];
        }
        /// <summary>
        /// Determines whether the specified key got pressed.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyPressed(Keys key)
        {
            return (!this._lastStates.ContainsKey(key) || !this._lastStates[key]) && 
                this._keyStates.ContainsKey(key) &&
                this._keyStates[key];
        }
        /// <summary>
        /// Determines whether the specified key got released.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyReleased(Keys key)
        {
            return this._lastStates.ContainsKey(key) && this._lastStates[key] &&
                (!this._keyStates.ContainsKey(key) || !this._keyStates[key]);
        }
        /// <summary>
        /// Gets the pressed keys.
        /// </summary>
        public IEnumerable<Keys> GetPressedKeys()
        {
            return this._keyStates
                .Where(pair => pair.Value && !this._lastStates.ContainsKey(pair.Key) || !this._lastStates[pair.Key])
                .Select(pair => pair.Key);
        }
        /// <summary>
        /// Gets the pressed keys.
        /// </summary>
        public IEnumerable<Keys> GetReleasedKeys()
        {
            return this._lastStates
                .Where(pair => pair.Value && !this._keyStates.ContainsKey(pair.Key) || !this._keyStates[pair.Key])
                .Select(pair => pair.Key);
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            try
            {
                this.UpdateStates();
            }
            catch (InvalidOperationException ex)
            {
                this.EventManager.Publish(new ExceptionEvent(ex));
            }
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public void Render()
        {
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            GameLoop loop = XGL.Components.Get<GameLoop>();
            loop.Subscribe(this);
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
            this.SetKeyState((Keys)e.KeyCode, true);
            this.EventManager.Publish(new KeyDownEvent((Keys)e.KeyCode));
        }
        /// <summary>
        /// Handles the KeyUp event of the surface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SurfaceKeyUp(object sender, KeyEventArgs e)
        {
            this.SetKeyState((Keys)e.KeyCode, false);
            this.EventManager.Publish(new KeyUpEvent((Keys)e.KeyCode));
        }
        #endregion
    }
}
