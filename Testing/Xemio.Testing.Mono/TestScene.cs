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
        private Vector2 _velocity;

        private IBrush _brush;

        public TestScene()
        {
        }

        public override void LoadContent ()
        {
            this._brush = this.RenderFactory.CreateSolidBrush(Color.Red);
        }

        public override void Tick(float elapsed)
        {
            this._position += this._velocity;

            if (this._position.X > this.GraphicsDevice.DisplayMode.Width - 50)
                this._velocity = new Vector2(-1, this._velocity.Y);

            if (this._position.X <= 0)
                this._velocity = new Vector2(1, this._velocity.Y);

            if (this._position.Y > this.GraphicsDevice.DisplayMode.Height - 50)
                this._velocity = new Vector2(this._velocity.X, -1);

            if (this._position.Y <= 0)
                this._velocity = new Vector2(this._velocity.X, 1);
        }

        public override void Render()
        {
            using (this.RenderManager.Tint(Color.Red, BlendMode.Override))
            using (this.RenderManager.Translate(this._position))
            {
                this.RenderManager.FillRectangle(this._brush, new Rectangle(0, 0, 50, 50));
            }
        }
    }
}

