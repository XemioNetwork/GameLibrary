using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Rendering.Sprites;
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
            var server = new Server("tcp://8000");

            while (Console.ReadKey().Key == ConsoleKey.R)
            {
                for (int i = 0; i < 20; i++)
                {
                    var c = new Client("tcp://127.0.0.1:8000");
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(2000);
                        c.Close();
                    });
                }
            }
        }
        #endregion
    }
}
