using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Localization;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.HTML5;

namespace Xemio.Contrib.Testing.HTML5
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var config = XGL.Configure()
                .FrameRate(60)
                .BackBuffer(800, 600)
                .Components(
                    new WebSurface("surface"),
                    new WebGameLoop(),
                    new EventManager(),
                    new SceneManager(),
                    new InputManager(),
                    new ContentManager(),
                    new ImplementationManager(),
                    new LocalizationManager(),
                    new GlobalExceptionHandler(),
                    new GameTime(),
                    new LibraryLoader())
                .Graphics<HTMLGraphicsInitializer>()
                .Scenes(new TestScene())
                .BuildConfiguration();

            XGL.Run(config);
        }
    }
}
