using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Sound.Loops
{
    public class LoopManager : IGameHandler, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LoopManager"/> class.
        /// </summary>
        public LoopManager()
        {
            this._controllers = new Dictionary<ISound, LoopController>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<ISound, LoopController> _controllers;
        #endregion

        #region Methods
        /// <summary>
        /// Registers the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public void Register(ISound sound)
        {
            if (!this._controllers.ContainsKey(sound))
            {
                this._controllers.Add(sound, new LoopController(sound));
            }
        }
        /// <summary>
        /// Removes the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public void Remove(ISound sound)
        {
            this._controllers.Remove(sound);
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            foreach (LoopController controller in this._controllers.Values)
            {
                controller.Tick(elapsed);
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
    }
}
