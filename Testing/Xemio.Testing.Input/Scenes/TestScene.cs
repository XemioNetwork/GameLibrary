using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Effects;

namespace Xemio.Testing.Input.Scenes
{
    public class TestScene : Scene
    {
        #region Fields
        private ContentReference<ITexture> _texture;
        private ContentReference<Color> _color;
        private ContentReference<Vector2> _offset;
        #endregion

        #region Overrides of Scene
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        public override void LoadContent()
        {
            this._texture = this.ContentManager.Get<ITexture>(@"test.png");
            this._color = this.ContentManager.Get<Color>(@"test.color");
            this._offset = this.ContentManager.Get<Vector2>(@"test.offset");
        }
        /// <summary>
        /// Handles a game render.
        /// </summary>
        public override void Render()
        {
            base.Render();

            using (this.RenderManager.Tint(this._color))
            using (this.RenderManager.Translate(this._offset))
            {
                this.RenderManager.Render(this._texture.Value, new Vector2(0, 0));
            }
        }
        #endregion
    }
}
