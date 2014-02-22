using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    internal class Local
    {
        #region Fields
        private static readonly Dictionary<string, LocalBridge> _bridges = new Dictionary<string, LocalBridge>();
        #endregion
        
        #region Methods
        /// <summary>
        /// Gets a local bridge for the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public static LocalBridge Bridge(string name)
        {
            lock (_bridges)
            {
                if (!_bridges.ContainsKey(name))
                {
                    _bridges[name] = new LocalBridge();
                }

                return _bridges[name];
            }
        }
        #endregion
    }
}
