using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.HTML5
{
    public class HTMLTexture : ITexture
    {
        #region Properties
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public dynamic Texture { get; set; }
        #endregion

        #region Implementation of ITexture
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetData(byte[] data)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        #endregion
    }
}
