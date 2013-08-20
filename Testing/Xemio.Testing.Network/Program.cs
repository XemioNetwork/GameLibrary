using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Events;
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
        [STAThread]
        static void Main(string[] args)
        {
            var t = new ThreadInvoker();

            for (int i = 0; i < 20; i++)
                t.Invoke(() => Console.WriteLine("Invoked: {0}", i));

            var config = XGL.Configure()
                .DefaultComponents()
                .DefaultInput()
                .DisableSplashScreen()
                .BuildConfiguration();
            
            XGL.Run(new TestForm(), config);
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
