﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Client
{
    public class ClientSendingPackageEvent : ClientPackageEvent, IInterceptableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPackageEvent"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public ClientSendingPackageEvent(IClient client, Package package) : base(client, package)
        {
        }
        #endregion

        #region Implementation of IInterceptableEvent
        /// <summary>
        /// Gets a value indicating whether the event propagation was canceled.
        /// </summary>
        public bool IsCanceled { get; private set; }
        /// <summary>
        /// Cancels the event propagation.
        /// </summary>
        public void Cancel()
        {
            this.IsCanceled = true;
        }
        #endregion
    }
}