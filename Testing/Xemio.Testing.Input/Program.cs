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

            s.Save(new TestClass()
            {
                A = 33,
                B = "Hallo Welt",
                C = new TestImpl()
                {
                    IsTrue = true,
                    Content = "asdf",
                    SubClasses = new List<ISubClass>()
                    {
                        new SubClass() {IsTrue = false},
                        new TestImpl()
                        {
                            Content = "a",
                            SubClasses = new List<ISubClass>(),
                            IsTrue = false
                        }
                    }
                }
            }, memory, Format.Xml);

            memory.Position = 0;

            Console.WriteLine(streamReader.ReadToEnd());
            Console.ReadLine();
        }
    }

    public class TestClass
    {
        public int A { get; set; }
        public string B { get; set; }

        [Derivable]
        public ISubClass C { get; set; }
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

        [Derivable]
        public List<ISubClass> SubClasses { get; set; } 

        #endregion
    }
}
