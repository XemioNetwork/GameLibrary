using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Protocols.Local;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols.Tcp;

namespace Xemio.GameLibrary.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            ComponentManager.Instance.LoadComponents();

            ComponentManager.Get<PackageHandler>()
                .Subscribe<TestPackage>(this.Handle);

            LocalProtocol protocol = new LocalProtocol();

            TestServer server = new TestServer(protocol);
            TestClient client = new TestClient(protocol);

            client.Send(new TestPackage { Message = "Hallo Welt" });
            Console.ReadLine();
        }

        public void Handle(TestPackage package)
        {
            Console.WriteLine(package.Message);
        }
    }
}
