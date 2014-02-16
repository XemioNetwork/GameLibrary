using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Contrib.Testing.Xna
{
    public class TestScene : Scene
    {
        #region Constructors
        public TestScene()
        {

        }
        #endregion

        #region Fields
        private SpriteSheet _spritesheet;
        private IRenderTarget _renderTarget;
        private Vector2 _t;
        private float _e;
        #endregion

        #region Properties

        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._spritesheet = new SpriteSheet(this.Content.Load<ITexture>("res/tilesheet.png"), 32, 32);
            this._renderTarget = this.RenderFactory.CreateTarget(100, 100);
        }
        public override void Tick(float elapsed)
        {
            _e += elapsed;
            if (_e > 2000.0f)
            {
                this.Remove();
            }
            base.Tick(elapsed);
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this._t += new Vector2(1,1);
            using (this.GraphicsDevice.RenderTo(this._renderTarget))
            {
                this.RenderManager.Render(this._spritesheet, 555, Vector2.Zero);
            }

            this.RenderManager.Render(this._renderTarget, _t);
        }
        #endregion
    }
}
