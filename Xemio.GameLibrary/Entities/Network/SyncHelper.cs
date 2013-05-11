using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Network.Synchronization;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Network
{
    public static class SyncHelper
    {
        #region Methods
        /// <summary>
        /// Writes the specified entities.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="comparator">The comparator.</param>
        public static void Write(Stream stream, List<Entity> entities, IPropertyComparator comparator)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(entities.Count);

            foreach (Entity entity in entities)
            {
                writer.Write(entity.Id);
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
                    container.Storage.Save(stream, Properties.Changes);
                }
            }
        }
        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="environment">The environment.</param>
        public static IEnumerable<Entity> Read(Stream stream, EntityEnvironment environment)
        {
            BinaryReader reader = new BinaryReader(stream);
            int entityCount = reader.ReadInt32();

            for (int i = 0; i < entityCount; i++)
            {
                Entity entity = environment.GetEntity(reader.ReadInt32());

                if (entity == null) break;

                bool isDirty = reader.ReadBoolean();
                bool isDestroyed = reader.ReadBoolean();

                if (isDirty)
                {
                    entity.Position = reader.ReadVector2();
                }
                if (isDestroyed)
                {
                    entity.Destroy();
                }

                int containerCount = reader.ReadInt32();
                for (int j = 0; j < containerCount; j++)
                {
                    EntityDataContainer container = entity.GetContainer(reader.ReadInt32());
                    container.Storage.Load(stream);
                }

                yield return entity;
            }
        }
        #endregion
    }
}
