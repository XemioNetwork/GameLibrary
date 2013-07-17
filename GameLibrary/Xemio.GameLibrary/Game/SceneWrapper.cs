using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Game
{
    internal class SceneWrapper : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneWrapper"/> class.
        /// </summary>
        /// <param name="gameHandler">The game handler.</param>
        public SceneWrapper(IGameHandler gameHandler)
        {
            this.GameHandler = gameHandler;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the game handler.
        /// </summary>
        public IGameHandler GameHandler { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.GameHandler.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public override void Render()
        {
            this.GameHandler.Render();
        }
        #endregion
    }
}
