﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Entities.Data;

namespace Xemio.GameLibrary.Entities
{
    public class EntitySerializer : ContentSerializer<Entity>
    {
        #region Methods
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Entity Read(BinaryReader reader)
        {
            string typeName = reader.ReadString();
            Type type = Type.GetType(typeName);

            Entity entity = (Entity)Activator.CreateInstance(type);

            entity.EntityId = reader.ReadInt32();
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
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, Entity value)
        {
            Type type = value.GetType();
            string typeName = type.AssemblyQualifiedName;

            writer.Write(typeName);

            writer.Write(value.EntityId);
            writer.Write(value.Position);
            writer.Write(value.Containers.Count);

            foreach (EntityDataContainer container in value.Containers)
            {
                Type containerType = container.GetType();
                string containerTypeName = containerType.AssemblyQualifiedName;

                writer.Write(containerTypeName);
                writer.WriteInstance(container);
            }
        }
        #endregion
    }
}
