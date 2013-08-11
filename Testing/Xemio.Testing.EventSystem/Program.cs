using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Testing.EventSystem
{
    public class Test
    {
        public Test(int a, int b)
        {
            this.A = a;
            this.B = b;
        }

        public int A { get; private set; }
        public int B { get; private set; }

        public string C { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            BinaryReader reader = new BinaryReader(stream);
            
            writer.WriteInstance(new Test(3, 4) {C = "Hallo Welt"});
            stream.Seek(0, SeekOrigin.Begin);

            object value = reader.ReadInstance(typeof(Test));

            EventManager eventManager = new EventManager();
            eventManager.Subscribe<TestEvent>(Program.OnNext);
            eventManager.Subscribe<RedEvent>(Program.OnRed);

            eventManager.Publish(new TestEvent("Hallo Welt"));
            eventManager.Publish(new RedEvent("Hallo bloody Welt"));

            Console.ReadLine();
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
