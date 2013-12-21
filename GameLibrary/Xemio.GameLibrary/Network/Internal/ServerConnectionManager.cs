using System.Collections.Generic;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class ServerConnectionManager : Worker
    {
        #region Fields
        private readonly Server _server;
        private readonly IList<Worker> _processors; 
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionManager"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public ServerConnectionManager(Server server)
        {
            this._server = server;
            this._processors = new List<Worker>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new connection processor to handle incoming packages from a client.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void CreateConnectionProcessor(IConnection connection)
        {
            var processor = new ConnectionPackageProcessor(this._server, connection);
            processor.Start();

            this._processors.Add(processor);
        }
        #endregion

        #region Overrides of Worker
        /// <summary>
        /// Interrupts this worker.
        /// </summary>
        public override void Interrupt()
        {
            foreach (Worker processor in this._processors)
            {
                processor.Interrupt();
            }

            base.Interrupt();
        }
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            while (this.IsRunning() && this._server.Active)
            {
                IConnection connection = this._server.AcceptConnection();

                this._server.Connections.Add(connection);
                this._server.OnClientJoined(connection);

                this.CreateConnectionProcessor(connection);
            }
        }
        #endregion
    }
}
