using System.Collections.Generic;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Properties;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.Textures;

namespace Xemio.GameLibrary.Game.Scenes
{
    public class SplashScreen : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen"/> class.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public SplashScreen(IEnumerable<Scene> scenes)
        {
            this._scenes = scenes;
        }
        #endregion

        #region Fields
        private float _alpha;
        private float _elapsed;

        private ITexture _texture;
        private readonly IEnumerable<Scene> _scenes;
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._texture = this.TextureFactory.CreateTexture("intro", Resources.ResourceManager);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            if (this._elapsed >= 2000.0f)
            {
                this._alpha = MathHelper.Min(this._alpha + 0.025f, 1.0f);
                if (this._alpha >= 1.0f)
                {
                    this.SceneManager.Remove(this);
                    this.SceneManager.Add(this._scenes);
                }
            }
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            DisplayMode displayMode = this.GraphicsDevice.DisplayMode;

            this.GraphicsDevice.Clear(new Color(221, 221, 221));

            this.RenderManager.Render(this._texture,
                                      new Vector2(
                                          displayMode.Center.X - this._texture.Width * 0.5f,
                                          displayMode.Center.Y - this._texture.Height * 0.5f));

            Color color = new Color(0, 0, 0, this._alpha);
            IBrush brush = this.Geometry.Factory.CreateSolid(color);

            this.Geometry.FillRectangle(
                brush,
                new Rectangle(0, 0, displayMode.Width, displayMode.Height));
        }
        #endregion
    }
}
