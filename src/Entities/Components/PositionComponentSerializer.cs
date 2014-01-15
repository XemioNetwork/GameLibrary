using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Entities.Components
{
    public class PositionComponentSerializer : Serializer<PositionComponent>
    {
        #region Overrides of Serializer<PositionComponent>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override PositionComponent Read(IFormatReader reader)
        {
            return new PositionComponent {Value = reader.ReadVector2("Position")};
        }
        /// <summary>
        /// Writes the specified position component.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="positionComponent">The position component.</param>
        public override void Write(IFormatWriter writer, PositionComponent positionComponent)
        {
            writer.WriteVector2("Position", positionComponent.Value);
        }
        #endregion
    }
}
