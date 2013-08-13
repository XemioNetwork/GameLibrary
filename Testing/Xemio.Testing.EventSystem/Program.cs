using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Testing.EventSystem
{
    public class Program
    {
        private static IDisposable disposable;

        static void Main(string[] args)
        {
            var config = XGL.Configure()
                .DefaultComponents()
                .BuildConfiguration();

            XGL.Run(config);

            EventManager eventManager = XGL.Components.Get<EventManager>();
            disposable = eventManager.Subscribe<ExceptionEvent>(Program.OnException);

            throw new InvalidOperationException("Some exception text.");
        }

        private static void OnException(ExceptionEvent obj)
        {
            disposable.Dispose();
            Console.WriteLine(obj.Message);
        }

        public static void OnNext(TestEvent value)
        {
            Console.WriteLine(value.Message);
        }

        public static void OnRed(RedEvent value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
