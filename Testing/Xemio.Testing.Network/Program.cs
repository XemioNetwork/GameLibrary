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
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.Testing.Network
{
    static class Program
    {
        #region Entrance
        /// <summary>
        /// The program entry point.
        /// </summary>
        /// <param name="args">The args.</param>
        [STAThread]
        static void Main(string[] args)
        {
            var form = new TestForm();
            var config = XGL.Configure()
                .DefaultComponents()
                .DefaultInput()
                .Surface(form)
                .DisableSplashScreen()
                .BuildConfiguration();
            
            XGL.Run(config);
            Application.Run(form);
        }
        #endregion
    }
}
