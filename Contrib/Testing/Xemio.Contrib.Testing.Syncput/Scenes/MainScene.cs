using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Xemio.Contrib.Testing.Syncput.Entities;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Network.Syncput.Turns;

namespace Xemio.Contrib.Testing.Syncput.Scenes
{
    using Syncput = Xemio.GameLibrary.Network.Syncput.Syncput;

    public class MainScene : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainScene"/> class.
        /// </summary>
        public MainScene()
        {
            this._environment = new EntityEnvironment();
        }
        #endregion

        #region Fields
        private Syncput _syncput;
        private readonly EntityEnvironment _environment;
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._syncput = XGL.Components.Get<Syncput>();
            foreach (Player player in this._syncput.Lobby)
            {
                this._environment.Add(new TestEntity(player));
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._environment.Tick(elapsed);
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this._environment.Render();
        }
        #endregion
    }
}
