using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Network.SyncputServer.Scenes
{
    public abstract class ListScene : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ListScene"/> class.
        /// </summary>
        protected ListScene()
        {
        }
        #endregion

        #region Fields
        private int _selectedIndex;

        private SpriteFont _font;
        private IBrush _hover;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the index.
        /// </summary>
        public int SelectedIndex
        {
            get { return this._selectedIndex; }
            set { this._selectedIndex = (int)MathHelper.Clamp(value, this.MinIndex, this.MaxIndex - 1); }
        }
        /// <summary>
        /// Gets or sets the render offset.
        /// </summary>
        public Vector2 RenderOffset { get; set; }
        /// <summary>
        /// Gets the minimum index.
        /// </summary>
        public abstract int MinIndex { get; }
        /// <summary>
        /// Gets the maximum index.
        /// </summary>
        public abstract int MaxIndex { get; }
        /// <summary>
        /// Gets the first element.
        /// </summary>
        public virtual Vector2 FirstElement
        {
            get { return new Vector2(0, 11); }
        }
        /// <summary>
        /// Gets the message.
        /// </summary>
        public abstract string Message { get; }
        /// <summary>
        /// Gets the right scene.
        /// </summary>
        public virtual ListScene Right { get { return null; } }
        /// <summary>
        /// Gets the left scene.
        /// </summary>
        public virtual ListScene Left { get { return null; } }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._font = SpriteFontGenerator.Create("Courier New", 8);

            this._font.Spacing = 5;
            this._font.Kerning = -3;

            this._hover = this.GeometryFactory.CreateSolid(new Color(1, 1, 1, 0.2f));
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            //Clamp the selected index to its range.
            this.SelectedIndex = this.SelectedIndex;

            var inputManager = XGL.Components.Get<InputManager>();
            var playerInput = inputManager.PlayerInputs.First();

            if (playerInput.IsKeyPressed(Keys.Down))
                this.SelectedIndex++;

            if (playerInput.IsKeyPressed(Keys.Up))
                this.SelectedIndex--;

            if (playerInput.IsKeyPressed(Keys.Right) && this.Right != null)
                this.SceneManager.Add(new TransitionScene(TransitionDirection.Left, this, this.Right));

            if (playerInput.IsKeyPressed(Keys.Left) && this.Left != null)
                this.SceneManager.Add(new TransitionScene(TransitionDirection.Right, this, this.Left));
        }
        /// <summary>
        /// Renders this scene.
        /// </summary>
        public override void Render()
        {
            const string test = "abc";
            int height = (int)this._font.MeasureString(test).Y;

            if (this.MaxIndex > this.MinIndex)
            {
                this.GeometryManager.FillRectangle(
                    this._hover,
                    new Rectangle(
                        10 + this.FirstElement.X,
                        this.SelectedIndex * (height + 2) + this.FirstElement.Y,
                        this.GraphicsDevice.DisplayMode.Width - 20,
                        height + 3) + this.RenderOffset);
            }

            this.RenderManager.Render(this._font, this.Message, new Vector2(10, 10) + this.RenderOffset, Color.White);
        }
        #endregion
    }

}
