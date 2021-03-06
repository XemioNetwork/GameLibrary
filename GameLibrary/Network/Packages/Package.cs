﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Layouts.Generation;

namespace Xemio.GameLibrary.Network.Packages
{
    public abstract class Package : ILinkable
    {
        #region ILinkable<int> Member
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [Exclude]
        public int Id
        {
            get { return this.GetType().Name.GetHashCode(); }
        }
        #endregion
    }
}
