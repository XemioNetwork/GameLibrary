using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game.Scenes;

namespace Xemio.Testing.SceneManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var configuration = XGL.Configure()
                .WithDefaultComponents()
                .WithDefaultInput()
                .FrameRate(60)
                .Scenes(new MyScene())
                .BuildConfiguration();

            XGL.Run(configuration);

            Application.Run(new Form1());
        }
    }

    internal class MyScene : Scene
    {
        private float _elapsed;
        public override void Tick(float elapsed)
        {
            base.Tick(elapsed);

            this._elapsed += elapsed;

            Debug.WriteLine(this._elapsed);
        }
    }
}
