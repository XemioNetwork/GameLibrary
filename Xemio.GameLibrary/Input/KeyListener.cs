﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using System.Windows.Forms;
using Xemio.GameLibrary.Game;

namespace Xemio.GameLibrary.Input
{
    public class KeyListener : IComponent, IGameHandler, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyListener"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public KeyListener(IntPtr handle)
        {
            Control surface = Control.FromHandle(handle);

            surface.KeyDown += new KeyEventHandler(surface_KeyDown);
            surface.KeyUp += new KeyEventHandler(surface_KeyUp);

            this._keyStates = new Dictionary<Keys, bool>();
            this._lastStates = new Dictionary<Keys, bool>();
        }
        #endregion

        #region Fields
        private Dictionary<Keys, bool> _keyStates;
        private Dictionary<Keys, bool> _lastStates;
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
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            IEnumerator<KeyValuePair<Keys, bool>> enumerator = this._keyStates.GetEnumerator();

            this._lastStates.Clear();
            while (enumerator.MoveNext())
            {
                this._lastStates.Add(enumerator.Current.Key, enumerator.Current.Value);
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
            GameLoop loop = XGL.GetComponent<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the KeyDown event of the surface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void surface_KeyDown(object sender, KeyEventArgs e)
        {
            this.SetKeyState(e.KeyCode, true);
        }
        /// <summary>
        /// Handles the KeyUp event of the surface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void surface_KeyUp(object sender, KeyEventArgs e)
        {
            this.SetKeyState(e.KeyCode, false);
        }
        #endregion
    }
}
