using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Loading;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Properties;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Shapes;

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
        private Rectangle _rectangle;

        private readonly IEnumerable<Scene> _scenes;
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="loader">The content loader.</param>
        public override void LoadContent(IContentLoader loader)
        {
            DisplayMode displayMode = this.GraphicsDevice.DisplayMode;

            this._texture = this.Serializer.Load<ITexture>(Resources.intro.ToStream());
            this._rectangle = new Rectangle(0, 0, displayMode.Width, displayMode.Height);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            if (this._elapsed >= 2.0f)
            {
                this._alpha = MathHelper.Min(this._alpha + elapsed, 1.0f);
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

            this.GraphicsDevice.Render(this._texture,
                                       new Vector2(
                                          displayMode.Center.X - this._texture.Width * 0.5f,
                                          displayMode.Center.Y - this._texture.Height * 0.5f));

            var color = new Color(0, 0, 0, this._alpha);
            IBrush brush = this.GraphicsDevice.Factory.CreateSolidBrush(color);

            this.GraphicsDevice.FillRectangle(brush, this._rectangle);
        }
        #endregion
    }
}
