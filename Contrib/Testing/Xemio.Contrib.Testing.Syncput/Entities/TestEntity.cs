using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Contrib.Testing.Syncput.Entities
{
    using Syncput = Xemio.GameLibrary.Network.Syncput.Syncput;

    public class TestEntity : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestEntity"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public TestEntity(Player player)
        {
            this.Player = player;
            this.Renderer = new TestEntityRenderer(this);

            Syncput syncput = XGL.Components.Get<Syncput>();
            this.Color = new Color(
                syncput.SynchedRandom.NextFloat(),
                syncput.SynchedRandom.NextFloat(),
                syncput.SynchedRandom.NextFloat(),
                1.0f);

            GraphicsDevice graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            this.Position = graphicsDevice.DisplayMode.Center;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Player Player { get; private set; }
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            InputManager inputManager = XGL.Components.Get<InputManager>();
            PlayerInput input = inputManager[this.Player.PlayerIndex];

            const float speed = 4;

            if (input.IsKeyDown(Keys.Right))
                this.Position += new Vector2(1, 0) * speed;

            if (input.IsKeyDown(Keys.Left))
                this.Position += new Vector2(-1, 0) * speed;

            if (input.IsKeyDown(Keys.Up))
                this.Position += new Vector2(0, -1) * speed;

            if (input.IsKeyDown(Keys.Down))
                this.Position += new Vector2(0, 1) * speed;
        }
        #endregion
    }
}
