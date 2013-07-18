using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Subscribers;
using Xemio.GameLibrary.Entities.Network.Packages;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Network.Perceptions
{
    public class WorldUpdatePerception : PerceptionSubscriber<WorldUpdatePackage>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldExchangePerception"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public WorldUpdatePerception(EntityEnvironment environment)
        {
            this.Environment = environment;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the client receives a world exchange package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(Client client, WorldUpdatePackage package)
        {
            GameLoop loop = XGL.Components.Get<GameLoop>();

            float ticks = client.Latency / (float)loop.TargetTickTime;
            int tickCount = (int)MathHelper.Round(ticks);

            IEnumerable<Entity> entities = SyncHelper.Read(package.Stream, this.Environment);
            foreach (Entity entity in entities)
            {
                for (int i = 0; i < tickCount; i++)
                {
                    entity.Tick((float)loop.TargetTickTime);
                }
            }
        }
        #endregion
    }
}
