using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Common.Extensions;

namespace Xemio.GameLibrary.Network
{
    public abstract class Package : ILinkable
    {
        #region ILinkable<int> Member
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [ExcludeSync]
        public int Identifier
        {
            get { return this.GetType().Name.GetHashCode(); }
        }
        #endregion
    }
}
