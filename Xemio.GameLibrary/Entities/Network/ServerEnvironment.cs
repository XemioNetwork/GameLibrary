using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Entities.Network.Updates;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Entities.Network
{
    public class ServerEnvironment : EntityEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerEnvironment"/> class.
        /// </summary>
        public ServerEnvironment()
        {
            this.FrameDelay = 5;

            Server server = XGL.GetComponent<Server>();
            if (server == null)
            {
                throw new InvalidOperationException("You can not create a server environment on the client side.");
            }
        }
        #endregion

        #region Fields
        private int _frames;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the delay between world updates in frames.
        /// </summary>
        public int FrameDelay { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a server snapshot.
        /// </summary>
        public virtual IWorldUpdate CreateUpdate()
        {
            return new WorldUpdatePackage(this);
        }
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Add(Entity entity)
        {
            base.Add(entity);

            EntityPackage package = new EntityPackage(entity);
            Server server = XGL.GetComponent<Server>();

            server.Send(package);
        }
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.BeginEnumeration();
            foreach (Entity entity in this.Entities)
            {
                bool enabledState = entity.HandleComponentTick;

                entity.HandleComponentTick = false;
                entity.Tick(elapsed);

                foreach (EntityComponent component in entity.Components)
                {
                    NetworkComponent networkComponent = component as NetworkComponent;
                    if (networkComponent != null)
                    {
                        networkComponent.HostTick(elapsed);
                    }
                }

                if (entity.IsDestroyed)
                {
                    this.Remove(entity);
                }

                entity.HandleComponentTick = enabledState;
            }

            this.EndEnumeration();

            this._frames++;
            if (this._frames >= this.FrameDelay)
            {
                this._frames = 0;

                Server server = XGL.GetComponent<Server>();
                IWorldUpdate update = this.CreateUpdate();
                Package package = update as Package;

                if (package == null)
                {
                    throw new InvalidOperationException("The world update has to be transformable into a package instance.");
                }

                server.Send(package);
            }
        }
        #endregion
    }
}
