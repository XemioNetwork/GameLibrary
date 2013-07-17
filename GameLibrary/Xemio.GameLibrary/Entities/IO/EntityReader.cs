using System;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Common.Extensions;

namespace Xemio.GameLibrary.Entities.IO
{
    public class EntityReader : ContentReader<Entity>
    {
        #region Overrides of ContentReader<Entity>
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Entity Read(BinaryReader reader)
        {
            string typeName = reader.ReadString();
            Type type = Type.GetType(typeName);

            Entity entity = EntityFactory.Instance.CreateEntity(type);
            entity.Position = reader.ReadVector2();

            int containerCount = reader.ReadInt32();
            for (int i = 0; i < containerCount; i++)
            {
                string containerTypeName = reader.ReadString();
                Type containerType = Type.GetType(containerTypeName);

                var container = reader.ReadInstance(containerType) as EntityDataContainer;
                entity.Containers.Add(container);
            }

            return entity;
        }
        #endregion
    }
}
