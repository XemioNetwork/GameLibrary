using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary.Entities.Network;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Protocols.Local;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game;

namespace Xemio.Testing.Network
{
    class Program : IGameHandler
    {
        #region Entrance
        /// <summary>
        /// The program entry point.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
        #endregion

        #region Fields
        private ServerEnvironment _environment;
        private ClientEnvironment _clientEnvironment;
        #endregion

        #region Methods
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            Form form = new Form();
            
            LocalProtocol.GetServer().Latency = 100;
            LocalProtocol.GetClient().Latency = 100;

            Server server = new Server(LocalProtocol.GetServer());
            Client client = new Client(LocalProtocol.GetClient());

            this._environment = new ServerEnvironment();
            this._clientEnvironment = new ClientEnvironment();

            TestEntity entity = new TestEntity();
            this._environment.Add(entity);

            XGL.Components.Get<GameLoop>().Subscribe(this);

            Application.Run(form);
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            this._environment.Tick(elapsed);
            this._clientEnvironment.Tick(elapsed);
        }
        /// <summary>
        /// Handles render calls.
        /// </summary>
        public void Render()
        {

        }
        #endregion
    }
}
