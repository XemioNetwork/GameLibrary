using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Assets.Serialization
{
    public class AssetContainerSerializer : ContentSerializer<AssetContainer>
    {
        #region Overrides of ContentSerializer<AssetContainer>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override AssetContainer Read(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, AssetContainer value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
