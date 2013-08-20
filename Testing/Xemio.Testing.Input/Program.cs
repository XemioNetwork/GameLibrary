using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Testing.Input
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            
            XGL.Run(XGL.Configure()
                .DefaultInput()
                .Components(
                    new ImplementationManager(),
                    new ContentManager(),
                    new EventManager())
                .DisableSplashScreen()
                .BuildConfiguration());

            var memoryStream = new MemoryStream();
            var content = XGL.Components.Get<ContentManager>();

            content.Save(new Test() {A = 17, Sub = new SubClass(1337)}, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            Test t = content.Load<Test>(memoryStream);

            string a = "3";
        }
    }
    class Test
    {
        public int A { get; set; }
        public SubClass Sub { get; set; }
    }
    class SubClass
    {
        public SubClass(int b)
        {
            this.B = b;
        }

        public int B { get; private set; }
    }
}
