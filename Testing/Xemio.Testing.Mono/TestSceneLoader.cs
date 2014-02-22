using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Scenes.Transitions;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.Testing.Mono
{
    public class TestSceneLoader : LoadingScene
    {
        #region Overrides of SceneContainer
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingScene"/> class.
        /// </summary>
        public TestSceneLoader() : base(new TestScene())
        {
        }

        private IFont _font;

        /// <summary>
        /// Loads the scene content.
        /// </summary>
        /// <param name="loader">The content loader.</param>
        public override void LoadContent(ContentLoader loader)
        {
            this._font = this.GraphicsDevice.RenderFactory.CreateFont("Arial", 8);
        }
        
        /// <summary>
        /// Called when the scene is loaded.
        /// </summary>
        protected override void OnCompleted()
        {
            this.TransitionTo(this.Target, new SlideTransition(Vector2.Up + Vector2.Left, 1000));
        }
        /// <summary>
        /// Handles a game render.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.Clear(Color.Red);

            var builder = new StringBuilder();
            foreach (string value in this.CompletedElements)
            {
                builder.AppendLine(value);
            }

            builder.AppendLine(this.CurrentElement + "...");

            this.GraphicsDevice.TextRasterizer.Render(this._font, builder.ToString(), new Vector2(5, 5));
        }
        #endregion
    }
}
