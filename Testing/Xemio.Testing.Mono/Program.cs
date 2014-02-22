using System;
using Xemio.GameLibrary;
using System.Windows.Forms;
using System.Drawing;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Rendering.GdiPlus;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Common.Threads;

namespace Xemio.Testing.Mono
{
    public class BufferedForm : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Xemio.Testing.Mono.BufferedForm"/> class.
        /// </summary>
        public BufferedForm()
        {
            this.DoubleBuffered = true;    
        }
        #endregion
    }

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var form = new BufferedForm();
            form.ClientSize = new Size(800, 600);  
            form.Text = "XGL - Mono Test";   
            
            var config = XGL.Configure()
               .DisableSplashScreen()
               .BackBuffer(form.ClientSize.Width / 2,
                           form.ClientSize.Height / 2)
               .FrameRate(60)
               .Graphics<GdiGraphicsInitializer>()
               .Content(Format.Xml)
               .Surface(form)
               .Scenes(new TestSceneLoader())
               .CreatePlayerInput()
               .BuildConfiguration();

            XGL.Run(config);
            
            var server = new Server("local://test");
            var client = new Client("local://test");

            Application.Run(form);
            Application.Exit();
        }
    }
}

