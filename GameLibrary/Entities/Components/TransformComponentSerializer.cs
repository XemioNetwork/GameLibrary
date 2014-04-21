using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Layouts;

namespace Xemio.GameLibrary.Entities.Components
{
    public class TransformComponentSerializer : LayoutSerializer<TransformComponent>
    {
        #region Overrides of Serializer<PositionComponent>
        /// <summary>
        /// Creates the layout.
        /// </summary>
        public override PersistenceLayout<TransformComponent> CreateLayout()
        {
            return new PersistenceLayout<TransformComponent>()
                .Element("Rotation", component => component.Rotation)
                .Element("Scale", component => component.Scale)
                .Element("Position", component => component.Position);
        }
        #endregion
    }
}
