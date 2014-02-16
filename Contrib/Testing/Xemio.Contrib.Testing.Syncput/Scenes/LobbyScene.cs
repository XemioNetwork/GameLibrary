using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Syncput;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Syncput.Packages.Requests;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Fonts;
using Keys = Xemio.GameLibrary.Input.Keys;

namespace Xemio.Contrib.Testing.Syncput.Scenes
{
    using Syncput = Xemio.GameLibrary.Network.Syncput.Syncput;

    public class LobbyScene : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyScene"/> class.
        /// </summary>
        public LobbyScene()
        {
        }
        #endregion

        #region Fields
        private SpriteFont _font;
        private Syncput _syncput;
        #endregion
        
        #region Methods
        public override void LoadContent()
        {
            this._font = SpriteFontGenerator.Create("Courier New", 10);
            this._font.Spacing = 5;
            this._font.Kerning = -5;

            string playerName = string.Empty;
            Utility.InputBox("Player name", "Name:", ref playerName);

            this._syncput = XGL.Components.Get<Syncput>();
            this._syncput.Connect("91.66.124.206", 15565, playerName);
        }

        public override void Tick(float elapsed)
        {
            var inputManager = XGL.Components.Get<InputManager>();
            var playerInput = inputManager.PlayerInputs.First();

            var syncputClient = XGL.Components.Get<SyncputClient>();

            if (playerInput.IsKeyPressed(Keys.Enter))
            {
                string message = string.Empty;
                if (Utility.InputBox("Send message", "Message:", ref message) == DialogResult.OK)
                {
                    this._syncput.Console.Send(syncputClient.PlayerName, message);
                }
            }

            if (this._syncput.IsGameStarted())
            {
                this.SceneManager.Add(new MainScene());
                this.Remove();
            }
        }
        public override void Render()
        {
            this.GraphicsDevice.Clear(new Color(100, 100, 100));
            
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Players:");
            builder.AppendLine("---------------------");

            foreach (Player player in this._syncput.Lobby)
            {
                builder.AppendLine(player.Name);
            }
            
            string message = builder.ToString().Replace(Environment.NewLine, "\n");

            this.RenderManager.Render(this._font, message, new Vector2(10, 11), Color.Black);
            this.RenderManager.Render(this._font, message, new Vector2(10, 10), Color.White);
        }
        #endregion
    }
}
