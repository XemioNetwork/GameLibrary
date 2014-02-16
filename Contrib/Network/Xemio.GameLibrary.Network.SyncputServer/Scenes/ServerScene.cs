using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Syncput;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Network.SyncputServer.Scenes
{
    using Syncput = Xemio.GameLibrary.Network.Syncput.Syncput;

    public class ServerScene : ListScene
    {
        #region Properties
        /// <summary>
        /// Gets the minimum index.
        /// </summary>
        public override int MinIndex
        {
            get { return 0; }
        }
        /// <summary>
        /// Gets the maximum index.
        /// </summary>
        public override int MaxIndex
        {
            get
            {
                var sync = XGL.Components.Get<Syncput>();
                return sync.Lobby.Count;
            }
        }
        /// <summary>
        /// Gets the first element.
        /// </summary>
        public override Vector2 FirstElement
        {
            get { return new Vector2(0, 59); }
        }
        /// <summary>
        /// Gets the message.
        /// </summary>
        public override string Message
        {
            get
            {
                Syncput sync = XGL.Components.Get<Syncput>();
                IServer server = XGL.Components.Get<IServer>();

                string message = string.Format(
                    "SYNCPUT Server v0.3 \n" +
                    "------------------------------- \n" +
                    "Players: {0}/{1} \n" +
                    "------------------------------- \n",
                    sync.Lobby.Count,
                    sync.Lobby.MaxPlayers);

                for (int i = 0; i < sync.Lobby.MaxPlayers; i++)
                {
                    if (sync.Lobby.Count > i)
                    {
                        Player player = sync.Lobby.ElementAt(i);
                        IConnection connection = server.Connections[i];

                        message += string.Format("{0}. {1}", i + 1, player.Name);
                        message += string.Format(" ({0}ms)", connection.Latency);
                    }
                    else
                    {
                        message += " ";
                    }

                    message += "\n";
                }

                message += " \n";
                message += "Messages: \n";
                message += "------------------------------- \n";

                IList<string> consoleMessages = sync.Console.GetMessages();
                for (int i = 0; i < sync.Console.Capacity; i++)
                {
                    if (consoleMessages.Count > i)
                    {
                        message += consoleMessages[i] + "\n";
                        continue;
                    }

                    message += " \n";
                }

                return message;
            }
        }
        /// <summary>
        /// Gets the right scene.
        /// </summary>
        public override ListScene Right
        {
            get
            {
                Syncput sync = XGL.Components.Get<Syncput>();
                if (sync.Lobby.Count == 0)
                    return null;

                Player player = sync.Lobby.ElementAt(this.SelectedIndex);
                return new PlayerOptionsScene(player);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            InputManager inputManager = XGL.Components.Get<InputManager>();
            PlayerInput input = inputManager.PlayerInputs.First();

            if (input.IsKeyPressed(Keys.G))
            {
                Syncput sync = XGL.Components.Get<Syncput>();
                sync.StartGame();
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
