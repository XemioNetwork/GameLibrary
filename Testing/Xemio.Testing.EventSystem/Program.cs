using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
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
            throw new InvalidOperationException("Some exception text.");
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
