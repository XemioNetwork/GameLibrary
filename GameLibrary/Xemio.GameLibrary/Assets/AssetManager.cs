using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Assets
{
    public class AssetManager : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetManager"/> class.
        /// </summary>
        public AssetManager()
        {
            this.RootDirectory = "./Assets/";
        }
        #endregion

        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the root directory.
        /// </summary>
        public string RootDirectory { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
