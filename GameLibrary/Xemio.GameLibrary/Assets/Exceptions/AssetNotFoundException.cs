using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Assets.Exceptions
{
    public class AssetNotFoundException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetNotFoundException"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public AssetNotFoundException(string id)
            : base(string.Format("Asset for the id '{0}' does not exist.", id))
        {
            this.Id = id;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id { get; private set; }
        #endregion
    }
}
