using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network
{
    public static class NetworkHelper
    {
        #region Methods
        /// <summary>
        /// Determines whether this XGL instance is server-sided.
        /// </summary>
        public static bool IsServer()
        {
            return XGL.GetComponent<Server>() != null;
        }
        #endregion
    }
}
