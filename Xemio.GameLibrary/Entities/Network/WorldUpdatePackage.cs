using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Network.Packages
{
    internal class WorldUpdatePackage : Package, IWorldUpdate
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

        #region Properties
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public byte[] Data { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the snapshot using the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        private void Create(EntityEnvironment environment)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(memory);
                List<Entity> syncedEntities = environment.Where(e => e.IsSynced).ToList();

                writer.Write(syncedEntities.Count);

                foreach (Entity entity in syncedEntities)
                {
                    writer.Write(entity.ID);
                    writer.Write(entity.IsDirty);
                    writer.Write(entity.IsDestroyed);

                    if (entity.IsDirty)
                    {
                        writer.Write(entity.Position);
                    }

                    writer.Write(entity.Containers.Count);
                    foreach (EntityDataContainer container in entity.Containers)
                    {
                        writer.Write(container.ID);
                        container.Storage.SaveChanges(memory);
                    }
                }

                this.Data = memory.ToArray();
            }
        }
        #endregion

        #region IWorldUpdate Member
        /// <summary>
        /// Applies the snapshot to the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public void Apply(EntityEnvironment environment)
        {
            using (MemoryStream stream = new MemoryStream(this.Data))
            {
                BinaryReader reader = new BinaryReader(stream);
                int entityCount = reader.ReadInt32();

                GameLoop loop = XGL.GetComponent<GameLoop>();
                Client client = XGL.GetComponent<Client>();

                float ticks = client.Latency / (float)loop.TargetTickTime;
                int tickCount = (int)MathHelper.Round(ticks);

                for (int i = 0; i < entityCount; i++)
                {
                    Entity entity = environment.GetEntity(reader.ReadInt32());
                    bool isDirty = reader.ReadBoolean();
                    bool isDestroyed = reader.ReadBoolean();

                    if (entity == null) return;

                    if (isDirty)
                    {
                        entity.Position = reader.ReadVector2();
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

                    int containerCount = reader.ReadInt32();
                    for (int j = 0; j < containerCount; j++)
                    {
                        EntityDataContainer container = entity.GetContainer(reader.ReadInt32());
                        container.Storage.LoadChanges(stream);
                    }
                }
            }
        }
        #endregion

    }
}
