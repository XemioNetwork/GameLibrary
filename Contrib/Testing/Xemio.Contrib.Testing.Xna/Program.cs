using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Rendering.Xna;

namespace Xemio.Contrib.Testing.Xna
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

            GameForm form = new GameForm();
             
            var config = XGL.Configure()
                .FrameRate(60)
                .BackBuffer(form.ClientSize)
                .DefaultComponents()
                .DefaultInput()
                .Graphics<GDIGraphicsInitializer>()
                .Surface(form)
                .Scenes(new TestScene())
                .BuildConfiguration();

            XGL.Run(config);
            Application.Run(form);
        }
    }
}
