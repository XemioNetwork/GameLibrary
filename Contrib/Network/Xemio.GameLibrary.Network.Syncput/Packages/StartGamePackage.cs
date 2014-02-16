using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Syncput.Packages
{
    public class StartGamePackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StartGamePackage"/> class.
        /// </summary>
        public StartGamePackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StartGamePackage"/> class.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        public StartGamePackage(long startIndex)
        {
            this.StartIndex = startIndex;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the start index.
        /// </summary>
        public long StartIndex { get; private set; }
        #endregion
    }
}
