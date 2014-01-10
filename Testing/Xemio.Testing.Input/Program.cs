﻿using System;
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
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Timing;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Script;

namespace Xemio.Testing.Input
{
    class Program
    {
        static void Main(string[] args)
        {
            var im = XGL.Components.Get<ImplementationManager>();

            while (true)
            {
                Console.ReadLine();
                XGL.Components.Get<ScriptExecutor>().Send(new TestEvent(), "asdf");
            }

            /*var memoryStream = new MemoryStream();

            var serializer = XGL.Components.Get<SerializationManager>();

            var entity = new Parent()
            {
                Child = new TestClass()
                {
                    A = ""
                },
                ChildB = new TestClassImpl2()
                {
                    A = "Welt",
                    G = 0.077
                }
            };
            
            serializer.Save(entity, memoryStream, Format.Xml);
            memoryStream.Position = 0;

            string content = new StreamReader(memoryStream).ReadToEnd();*/
        }
    }

    public class TestEvent : IEvent
    {
        #region Overrides of Object
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return "Hallo Welt";
        }
        #endregion
    }

    public interface ITest
    {
        string A { get; set; }
    }

    public class Parent
    {
        public ITest Child { get; set; }
        public ITest ChildB { get; set; }
    }

    public class TestClass : ITest
    {
        #region Constructors

        public TestClass()
        {
            this.B = 7;
        }
        #endregion

        #region Implementation of ITest

        public string A { get; set; }
        private int B { get; set; }

        #endregion
    }

    public class TestClassImpl2 : ITest
    {
        #region Implementation of ITest

        public string A { get; set; }
        public double G { get; set; }

        #endregion
    }
}
