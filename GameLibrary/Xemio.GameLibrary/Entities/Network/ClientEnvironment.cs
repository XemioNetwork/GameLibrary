using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Entities.Network.Packages;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Entities.Network.Perceptions;

namespace Xemio.GameLibrary.Entities.Network
{
    public class ClientEnvironment : EntityEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientEnvironment"/> class.
        /// </summary>
        public ClientEnvironment()
        {
            Client client = XGL.Components.Get<Client>();
            if (client == null)
            {
                throw new InvalidOperationException("You can not create a client environment on the server side.");
            }

            client.Subscribe(new EntityCreationPerception(this));
            client.Subscribe(new WorldExchangePerception(this));
            client.Subscribe(new WorldUpdatePerception(this));
        }
        #endregion
    }
}
