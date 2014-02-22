using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes
{
    public class ColorScene : Scene
    {
        #region Consructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorScene" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public ColorScene(Color color)
        {
            this.Color = color;
        }
        #endregion

        #region Fields
        private IBrush _solidBrush;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; private set; }
        #endregion

        #region Overrides of SceneContainer
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        /// <param name="loader">The content loader.</param>
        public override void LoadContent(ContentLoader loader)
        {
            this._solidBrush = this.GraphicsDevice.RenderFactory.CreateSolidBrush(this.Color);
        }
        /// <summary>
        /// Handles a game render.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.RenderManager.FillRectangle(this._solidBrush, this.GraphicsDevice.DisplayMode.Bounds);
        }
        #endregion
    }
}
