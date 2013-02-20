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
            
            TestServer server = new TestServer(new TcpServerProtocol(8000));

            TestClient client = new TestClient(new TcpClientProtocol());
            client.Protocol.Connect("127.0.0.1", 8000);

            client.Send(new TestPackage { Message = "Hallo Welt" });
            Console.ReadLine();
        }

        public void Handle(TestPackage package)
        {
            Console.WriteLine(package.Message);
        }
    }
}
