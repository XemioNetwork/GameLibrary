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
    public class PositionComponentSerializer : LayoutSerializer<PositionComponent>
    {
        #region Overrides of Serializer<PositionComponent>
        /// <summary>
        /// Creates the layout.
        /// </summary>
        public override PersistenceLayout<PositionComponent> CreateLayout()
        {
            return new PersistenceLayout<PositionComponent>()
                .Element("Position", component => component.Relative);
        }
        #endregion
    }
}
