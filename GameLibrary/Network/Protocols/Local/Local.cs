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
        private static readonly Dictionary<string, LocalChannel> _channels = new Dictionary<string, LocalChannel>();
        #endregion
        
        #region Methods
        /// <summary>
        /// Gets a local channel for the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public static LocalChannel GetChannel(string name)
        {
            lock (_channels)
            {
                if (!_channels.ContainsKey(name))
                {
                    _channels[name] = new LocalChannel();
                }

                return _channels[name];
            }
        }
        #endregion
    }
}
