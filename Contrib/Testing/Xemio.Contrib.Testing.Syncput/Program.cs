using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xemio.Contrib.Testing.Syncput.Scenes;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols.Local;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Rendering.Xna;

namespace Xemio.Contrib.Testing.Syncput
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             
            MainForm mainForm = new MainForm();
            
            var config = XGL.Configure()
                .DefaultComponents()
                .DefaultInput()
                .Surface(mainForm)
                .BackBuffer(mainForm.ClientSize)
                .FrameRate(60)
                .Scenes(new LobbyScene())
                .Graphics<GDIGraphicsInitializer>()
                .BuildConfiguration();
            
            XGL.Run(config);
            Application.Run(mainForm);
        }

        private static void OnException(ExceptionEvent ex)
        {
            throw ex.Exception;
        }
    }
}
