using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.GameLibrary.Entities.Network
{
    public class NetworkComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkComponent"/> class.
        /// </summary>
        public NetworkComponent(Entity entity) : base(entity)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick server-sided.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void HostTick(float elapsed)
        {
            this.Tick(elapsed);
        }
        #endregion
    }
}
