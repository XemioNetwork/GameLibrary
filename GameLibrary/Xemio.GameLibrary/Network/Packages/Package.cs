using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Network.Packages
{
    public abstract class Package : ILinkable
    {
        #region Methods
        /// <summary>
        /// Called before the package gets serialized.
        /// </summary>
        public virtual void OnSerialize()
        {
        }
        /// <summary>
        /// Called after the package get deserialized.
        /// </summary>
        public virtual void OnDeserialize()
        {
        }
        #endregion

        #region ILinkable<int> Member
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [ExcludeSync]
        public int Id
        {
            get { return this.GetType().Name.GetHashCode(); }
        }
        #endregion
    }
}
