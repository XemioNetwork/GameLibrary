using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Syncput.Consoles;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Network.Syncput.Logic.Client;
using Xemio.GameLibrary.Network.Syncput.Logic.Server;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Syncput.Packages.Requests;
using Xemio.GameLibrary.Network.Syncput.Turns;

namespace Xemio.GameLibrary.Network.Syncput
{
    public class Syncput : IComponent, IGameHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Syncput"/> class.
        /// </summary>
        public Syncput()
        {
            this.Console = new EmptyConsole();
            this.Lobby = new Lobby();

            this.SynchedRandom = new SeedableRandom();
            this.TurnSynchronizer = new TurnSynchronizer(this);

            this.GameStartIndex = -1;
            
            var gameLoop = XGL.Components.Get<GameLoop>();
            gameLoop.Subscribe(this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the console.
        /// </summary>
        public IConsole Console { get; private set; }
        /// <summary>
        /// Gets the lobby.
        /// </summary>
        public Lobby Lobby { get; internal set; }
        /// <summary>
        /// Gets the synched random.
        /// </summary>
        public SeedableRandom SynchedRandom { get; private set; }
        /// <summary>
        /// Gets the synchronizer.
        /// </summary>
        public TurnSynchronizer TurnSynchronizer { get; private set; }
        /// <summary>
        /// Gets the start index of the game.
        /// </summary>
        public long GameStartIndex { get; internal set; }
        #endregion
        
        #region Methods
        /// <summary>
        /// Determines whether the game got started.
        /// </summary>
        public bool IsGameStarted()
        {
            GameLoop gameLoop = XGL.Components.Get<GameLoop>();

            if (this.GameStartIndex == -1)
                return false;

            return gameLoop.FrameIndex >= this.GameStartIndex;
        }
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            if (this.GameStartIndex >= 0)
                return;

            IServer server = XGL.Components.Get<IServer>();
            GameLoop gameLoop = XGL.Components.Get<GameLoop>();

            int framesPerSecond = (int)(1000.0 / gameLoop.TargetFrameTime);
            this.GameStartIndex = gameLoop.FrameIndex + framesPerSecond * 2;

            foreach (SyncputConnection connection in server.Connections)
            {
                var package = new StartGamePackage(this.GameStartIndex + connection.Player.IndexInequality);
                server.Send(package, connection);
            }
        }
        /// <summary>
        /// Hosts at the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="seed">The seed.</param>
        /// <param name="maxPlayers">The max players.</param>
        public void Host(int port, int seed, int maxPlayers)
        {
            this.Host<TcpServerProtocol>(port, seed, maxPlayers);
        }
        /// <summary>
        /// Hosts the specified port.
        /// </summary>
        /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
        /// <param name="port">The port.</param>
        /// <param name="seed">The seed.</param>
        /// <param name="maxPlayers">The max players.</param>
        public void Host<TProtocol>(int port, int seed, int maxPlayers) where TProtocol : IServerProtocol, new()
        {
            IServer existingServer = XGL.Components.Get<IServer>();
            if (existingServer != null)
            {
                throw new InvalidOperationException("A server already exists inside the component registry. Syncput was unable to host a lobby.");
            }

            SyncputServer server = new SyncputServer(new TProtocol());
            server.Protocol.Host(port);

            this.Console = new ServerConsole(server, 5);
            this.Lobby = new Lobby {MaxPlayers = maxPlayers};

            this.SynchedRandom = new SeedableRandom {Seed = seed};

            XGL.Components.Add(server);
        }
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        /// <param name="playerName">Name of the player.</param>
        public void Connect(string ip, int port, string playerName)
        {
            this.Connect<TcpClientProtocol>(ip, port, playerName);
        }
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        /// <param name="playerName">Name of the player.</param>
        public void Connect<TProtocol>(string ip, int port, string playerName) where TProtocol : IClientProtocol, new()
        {
            IClient existingClient = XGL.Components.Get<IClient>();
            if (existingClient != null)
            {
                this.Disconnect();
            }
            
            IClient client = new SyncputClient(playerName, new TProtocol());
            client.Protocol.Connect(ip, port);

            GameLoop gameLoop = XGL.Components.Get<GameLoop>();
            gameLoop.LagCompensation = LagCompensation.None;

            this.Console = new ClientConsole(client, 5);

            XGL.Components.Add(client);
        }
        /// <summary>
        /// Disconnects the syncput client.
        /// </summary>
        public void Disconnect()
        {
            IClient existingClient = XGL.Components.Get<IClient>();

            if (existingClient != null)
            {
                if (existingClient.Protocol.Connected)
                {
                    existingClient.Protocol.Disconnect();
                }

                XGL.Components.Remove(existingClient);
            }
        }
        #endregion

        #region Implementation of IGameHandler
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            this.TurnSynchronizer.Tick(elapsed);
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public void Render()
        {
        }
        #endregion
    }
}
