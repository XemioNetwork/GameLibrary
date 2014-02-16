using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Syncput;
using Xemio.GameLibrary.Network.Syncput.Core;

namespace Xemio.GameLibrary.Network.SyncputServer.Scenes
{
    public class PlayerOptionsScene : ListScene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerOptionsScene"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public PlayerOptionsScene(Player player)
        {
            this.Player = player;

            IServer server = XGL.Components.Get<IServer>();
            IConnection connection = server.Connections
                .OfType<SyncputConnection>()
                .FirstOrDefault(c => player.Equals(c.Player));

            this.Connection = connection;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Player Player { get; private set; }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IConnection Connection { get; private set; }
        #endregion
        
        #region Overrides of ListScene
        /// <summary>
        /// Gets the minimum index.
        /// </summary>
        public override int MinIndex
        {
            get { return 2; }
        }
        /// <summary>
        /// Gets the maximum index.
        /// </summary>
        public override int MaxIndex
        {
            get { return 5; }
        }
        /// <summary>
        /// Gets the message.
        /// </summary>
        public override string Message
        {
            get
            {
                return this.Player.Name + "\n" +
                       "----------------------------- \n" +
                       "Kick Player \n" +
                       "Ban Player \n" +
                       "Manage permissions \n" +
                       "----------------------------- \n" +
                       this.Connection.IP + "\n" +
                       this.Connection.Latency + "ms\n";;
            }
        }
        /// <summary>
        /// Gets the left scene.
        /// </summary>
        public override ListScene Left
        {
            get { return new ServerScene(); }
        }
        /// <summary>
        /// Gets the right scene.
        /// </summary>
        public override ListScene Right
        {
            get
            {
                switch (this.SelectedIndex)
                {
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }

                return null;
            }
        }
        #endregion
    }
}
