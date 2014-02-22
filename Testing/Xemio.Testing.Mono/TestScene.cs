using System;
using System.Threading;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Scenes.Transitions;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Rendering.Effects;

namespace Xemio.Testing.Mono
{
    public class TestScene : Scene
    {
        private Vector2 _position;

        private IBrush _red;
        private IBrush _green;

        private ITexture _texture;

        public TestScene()
        {
        }

        public override void LoadContent(ContentLoader loader)
        {
            var files = new[]
            {
                @"C:\Users\Public\Pictures\Sample Pictures\Tulips.jpg",
                @"C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg",
                @"C:\Users\Public\Pictures\Sample Pictures\Jellyfish.jpg",
                @"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg",
                @"C:\Users\Public\Pictures\Sample Pictures\Lighthouse.jpg"
            };

            foreach (string fileName in files)
            {
                loader.Load<ITexture>(fileName, texture => this._texture = texture);
            }

            loader.Load("Really long stuff.", () => Thread.Sleep(4000));

            loader.Load("Yellow brush",
                () => this._red = this.RenderFactory.CreateSolidBrush(Color.Yellow));

            loader.Load("Really long stuff 2.", () => Thread.Sleep(2000));

            loader.Load("Lime brush",
                () => this._green = this.RenderFactory.CreateSolidBrush(Color.Lime));
        }

        public override void Tick(float elapsed)
        {
            if (this.Input.Get("key.left").IsActive)
                this._position += new Vector2(-1, 0);

            if (this.Input.Get("key.right").IsActive)
                this._position += new Vector2(1, 0);

            if (this.Input.Get("key.up").IsActive)
                this._position += new Vector2(0, -1);

            if (this.Input.Get("key.down").IsActive)
                this._position += new Vector2(0, 1);
        }

        public override void Render()
        {
            using (this.RenderManager.Translate(this._position))
            {
                this.RenderManager.Render(this._texture, new Rectangle(0, 0, 400, 300));
            }
        }
    }
}

