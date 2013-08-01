using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Entities
{
    public class EntityIdFactory
    {
        #region Fields
        private int _currentId;
        #endregion
        
        #region Methods
        /// <summary>
        /// Creates a new Id.
        /// </summary>
        public int CreateId()
        {
            return this._currentId++;
        }
        #endregion
    }
}
