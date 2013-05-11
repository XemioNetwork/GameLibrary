using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaTexture : ITexture
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaTexture"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public XnaTexture(Texture2D texture)
        {
            this.Texture = texture;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the internal texture.
        /// </summary>
        public Texture2D Texture { get; private set; }
        #endregion

        #region ITexture Member
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return this.Texture.Width; }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return this.Texture.Height; }
        }
        #endregion
    }
}
