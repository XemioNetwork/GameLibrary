using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Timing;

namespace Xemio.Testing.Network
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            Server server = ProtocolFactory.CreateServerFor<TcpServerProtocol>(8000);
            Client client = ProtocolFactory.CreateClientFor<TcpClientProtocol>("127.0.0.1", 8000);

            XGL.Components.Get<EventManager>()
                .Subscribe<ReceivedPackageEvent>(p =>
                {
                    Console.WriteLine(p.Package.GetType());
                });

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);

                int b = 0;
                while (true)
                {
                    Console.WriteLine(b++);

                    Thread.Sleep(1000);

                    client.Send(new LatencyPackage() { Latency = 720 });
                }
            });
        }
    }
}
