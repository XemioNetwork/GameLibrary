using System;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Rendering.Effects;

namespace Xemio.Testing.Mono
{
    public class TestScene : Scene
    {
        private Vector2 _position;

        private ITexture _texture;
        private IBrush _red;
        private IBrush _green;

        public TestScene()
        {
        }

        public override void LoadContent ()
        {
            this._texture = this.ContentManager.Get<ITexture>("/home/ckoebke/Downloads/meme.jpg").Value;
            this._red = this.RenderFactory.CreateSolidBrush(Color.Red);
            this._green = this.RenderFactory.CreateSolidBrush(Color.Lime);
        }

        public override void Tick(float elapsed)
        {
            if (this.Input.Get ("key.left").IsActive)
                this._position += new Vector2(-1, 0);

            if (this.Input.Get ("key.right").IsActive)
                this._position += new Vector2(1, 0);

            if (this.Input.Get ("key.up").IsActive)
                this._position += new Vector2(0, -1);

            if (this.Input.Get ("key.down").IsActive)
                this._position += new Vector2(0, 1);
        }

        public override void Render()
        {
            using (this.RenderManager.Tint(Color.Red, BlendMode.Override))
            using (this.RenderManager.Translate(this._position))
            {
                this.RenderManager.Render(this._texture, new Rectangle(-25, -25, 100, 100));
                this.RenderManager.FillEllipse(this._red, new Rectangle(50, 50, 50, 50));
                this.RenderManager.FillRectangle(this._green, new Rectangle(150, 50, 100, 30));
            }
        }
    }
}

