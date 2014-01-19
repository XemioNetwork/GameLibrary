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
using Xemio.GameLibrary.Script;

namespace Xemio.Testing.Input
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = XGL.Components.Get<SerializationManager>();
            var memory = new MemoryStream();
            var streamReader = new StreamReader(memory, Encoding.Default);
            
            Stopwatch watch = Stopwatch.StartNew();
            
            var entity = new Entity();
            entity.Position.Value = new Vector2(10, 10);

            s.Save(entity, memory, Format.Xml);

            memory.Position = 0;
            watch.Stop();

            Console.WriteLine(streamReader.ReadToEnd());
            Console.WriteLine();

            Console.WriteLine("Elapsed XGL Serialize: {0}ms", watch.Elapsed.TotalMilliseconds);

            /*watch = Stopwatch.StartNew();
            var seri = new XmlSerializer(typeof(TestClass));
            seri.Serialize(memory, new TestClass()
            {
                A = 33,
                B = "Hallo Welt",
                C = new SubClass()
                {
                    IsTrue = true
                }
            });
            watch.Stop();
            Console.WriteLine("Elapsed .NET Serialize: {0}ms", watch.Elapsed.TotalMilliseconds);*/

            Console.ReadLine();
        }
    }

    public class TestClass
    {
        public int A { get; set; }
        public string B { get; set; }

        [Tag("Subclasses")]
        [ElementTag("Subclass")]
        [Derivable]
        public List<ISubClass> C { get; set; }
    }

    public interface ISubClass
    {
        bool IsTrue { get; set; }
    }

    public class SubClass : ISubClass
    {
        public bool IsTrue { get; set; }
    }

    public class TestImpl : ISubClass
    {
        #region Implementation of ISubClass

        public bool IsTrue { get; set; }
        public string Content { get; set; }

        #endregion
    }
}
