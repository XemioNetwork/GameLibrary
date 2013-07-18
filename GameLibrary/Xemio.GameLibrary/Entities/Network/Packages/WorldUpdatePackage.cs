using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Entities.Network.Packages
{
    public class WorldUpdatePackage : BinaryPackage, IWorldUpdate
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldUpdatePackage"/> class.
        /// </summary>
        public WorldUpdatePackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldUpdatePackage"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public WorldUpdatePackage(EntityEnvironment environment)
        {
            this.Create(environment);
        }
        #endregion

        #region IWorldUpdate Member
        /// <summary>
        /// Creates the snapshot using the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public void Create(EntityEnvironment environment)
        {
            List<Entity> syncedEntities = environment
                .Where<Entity>(e => e.IsSynced)
                .ToList();

            SyncHelper.Write(this.Stream, syncedEntities, Properties.Changes);
        }
        /// <summary>
        /// Applies the snapshot to the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public void Apply(EntityEnvironment environment)
        {
            int entityCount = this.Reader.ReadInt32();

            GameLoop loop = XGL.Components.Get<GameLoop>();
            Client client = XGL.Components.Get<Client>();

            float ticks = client.Latency / (float)loop.TargetTickTime;
            int tickCount = (int)MathHelper.Round(ticks);

            for (int i = 0; i < entityCount; i++)
            {
                Entity entity = environment.GetEntity(this.Reader.ReadInt32());
                bool isDirty = this.Reader.ReadBoolean();
                bool isDestroyed = this.Reader.ReadBoolean();

                if (entity == null) return;

                if (isDirty)
                {
                    entity.Position = this.Reader.ReadVector2();
                }
                if (isDestroyed)
                {
                    entity.Destroy();
                }

                // Simulate past ticks that are already simulated server-sided
                // but got lost due to the client latency.
                for (int t = 0; t < tickCount; t++)
                {
                    entity.Tick((float)loop.TargetTickTime);
                }

                int containerCount = this.Reader.ReadInt32();
                for (int j = 0; j < containerCount; j++)
                {
                    EntityDataContainer container = entity.GetContainer(this.Reader.ReadInt32());
                    container.Storage.Load(this.Stream);
                }
            }
        }
        #endregion

    }
}
