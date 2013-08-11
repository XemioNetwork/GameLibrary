using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Network.Protocols.Local;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Timing;

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
        
        #region Methods
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            var control = new Control();
            var config = XGL.Configure()
                .WithDefaultComponents()
                .WithDefaultInput()
                .BuildConfiguration();

            XGL.Run(control.Handle, config);

            Server server = ProtocolFactory.CreateServerFor<TcpServerProtocol>(8000);
            Client client = ProtocolFactory.CreateClientFor<TcpClientProtocol>("127.0.0.1", 8000);

            Task.Factory.StartNew(() => A(client));
            Task.Factory.StartNew(() => B(server));

            while (true)
            {
                Console.Clear();
                Console.WriteLine(a);
                Thread.Sleep(1000);
            }
        }

        private int a;
        private void A(Client client)
        {
            while (true)
            {
                Thread.Sleep(16);
                client.Send(new LatencyPackage());
            }
        }
        private void B(Server server)
        {
            while (true)
            {
                Thread.Sleep(16);
                a++;
                server.Send(new TimeSyncPackage());
            }
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
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
