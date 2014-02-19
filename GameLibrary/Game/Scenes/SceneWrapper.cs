using Xemio.GameLibrary.Game.Handlers;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Game.Scenes
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
        /// <summary>
        /// Gets the tick handler.
        /// </summary>
        public ITickHandler TickHandler
        {
            get { return this.GameHandler as ITickHandler; }
        }
        /// <summary>
        /// Gets the render handler.
        /// </summary>
        public IRenderHandler RenderHandler
        {
            get { return this.GameHandler as IRenderHandler; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.TickHandler != null)
            {
                this.TickHandler.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public override void Render()
        {
            if (this.RenderHandler != null)
            {
                this.RenderHandler.Render();
            }
        }
        #endregion
    }
}
