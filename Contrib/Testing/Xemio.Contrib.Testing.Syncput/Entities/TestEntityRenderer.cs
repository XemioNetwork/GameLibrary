using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.Contrib.Testing.Syncput.Entities
{
    public class TestEntityRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestEntityRenderer"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public TestEntityRenderer(TestEntity entity) : base(entity)
        {
            this._font = SpriteFontGenerator.Create("Segoe UI", 8);
            this._font.Kerning = -3;

            ContentManager content = XGL.Components.Require<ContentManager>();

            this._texture = content.Load<ITexture>("Resources/boy.png");
            this._points = new List<Vector2>();
        }
        #endregion

        #region Fields
        private readonly SpriteFont _font;
        private readonly ITexture _texture;
        private readonly IBrush _brush;

        private readonly List<Vector2> _points; 
        #endregion

        #region Methods
        /// <summary>
        /// Renders this entity.
        /// </summary>
        public override void Render()
        {
            TestEntity testEntity = (TestEntity)this.Entity;

            this.RenderManager.Render(
                this._texture, testEntity.Position - new Vector2(
                    this._texture.Width * 0.5f,
                    this._texture.Height * 0.5f), testEntity.Color);

            this.RenderManager.Render(
                this._font,
                testEntity.Player.Name,
                testEntity.Position - new Vector2(14, 38));

            InputManager inputManager = XGL.Components.Get<InputManager>();
            PlayerInput input = inputManager[testEntity.Player.PlayerIndex];

            for (int i = 1; i < this._points.Count; i++)
            {
                this.GeometryManager.DrawLine(testEntity.Color, this._points[i], this._points[i - 1]);
            }

            this._points.Add(input.MousePosition);

            if (this._points.Count > 20)
                this._points.RemoveAt(0);

            GraphicsDevice graphicsDevice = XGL.Components.Require<GraphicsDevice>();
            IBrush brush = graphicsDevice.GeometryFactory.CreateSolid(testEntity.Color);

            this.GeometryManager.FillCircle(
                brush, input.MousePosition, 5);
        }
        #endregion
    }
}
