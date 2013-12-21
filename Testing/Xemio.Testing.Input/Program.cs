using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Testing.Input
{
    class Program
    {
        static void Main(string[] args)
        {
            var memoryStream = new MemoryStream();
            var serializer = XGL.Components.Get<SerializationManager>();
            
            var entity = new Entity();

            serializer.Save(entity, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var deserialized = serializer.Load<Entity>(memoryStream);

            string a = "3";
        }
    }

    interface ITest
    {
        string A { get; set; }
    }

    class Parent
    {
        public ITest Child { get; set; }
        public ITest ChildB { get; set; }
    }

    class TestClass : ITest
    {
        #region Implementation of ITest

        public string A { get; set; }
        public int B { get; set; }

        #endregion
    }

    class TestClassImpl2 : ITest
    {
        #region Implementation of ITest

        public string A { get; set; }
        public double G { get; set; }

        #endregion
    }
}
