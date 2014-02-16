using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Layouts;
using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.GdiPlus;
using Xemio.GameLibrary.Rendering.Initialization;
using Xemio.GameLibrary.Script;
using Xemio.Testing.Input.Scenes;

namespace Xemio.Testing.Input
{
    class Program
    {
        static void Main(string[] args)
        {
            var form = new InputForm();

            var config = XGL.Configure()
                .BackBuffer(form.ClientSize)
                .Content(Format.Xml)
                .CreatePlayerInput()
                .FrameRate(60)
                .Graphics<GdiGraphicsInitializer>()
                .Surface(form)
                .Scenes(new TestScene())
                .BuildConfiguration();

            XGL.Run(config);
            Application.Run(form);
        }
    }
}
