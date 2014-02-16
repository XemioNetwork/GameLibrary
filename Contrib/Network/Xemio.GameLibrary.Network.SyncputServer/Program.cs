using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Syncput;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Network.SyncputServer.Scenes;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Rendering.GdiPlus;

namespace Xemio.GameLibrary.Network.SyncputServer
{
    using Syncput = Xemio.GameLibrary.Network.Syncput.Syncput;

    public static class Program
    {
        static void Main(string[] args)
        {
            var form = new MainForm();
            var config = XGL.Configure()
                .DefaultComponents()
                .DefaultInput()
                .Scenes(new ServerScene())
                .Graphics<GdiGraphicsInitializer>()
                .BackBuffer(form.ClientSize)
                .Surface(form)
                .FrameRate(60)
                .BuildConfiguration();

            XGL.Run(config);

            IRandom random = new RandomProxy();

            Syncput syncput = XGL.Components.Get<Syncput>();
            syncput.Host(15565, random.Next(), 8);
            
            Application.Run(form);
        }
    }
}
