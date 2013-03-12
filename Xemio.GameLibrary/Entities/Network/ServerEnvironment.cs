using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Entities.Network.Packages;
using Xemio.GameLibrary.Network;

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
            if (this.Server == null)
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
        /// Gets the server.
        /// </summary>
        public Server Server
        {
            get { return XGL.GetComponent<Server>(); }
        }
        /// <summary>
        /// Gets or sets the delay between world updates in frames.
        /// </summary>
        public int FrameDelay { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Add(Entity entity)
        {
            base.Add(entity);

            EntityPackage package = new EntityPackage();
            package.ID = entity.ID;
            package.TypeName = entity.GetType().AssemblyQualifiedName;

            this.Server.Send(package);
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Remove(Entity entity)
        {
            base.Remove(entity);
        }
        /// <summary>
        /// Creates a server snapshot.
        /// </summary>
        public virtual IWorldUpdate CreateUpdate()
        {
            return new UpdatePackage(this);
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

                IWorldUpdate update = this.CreateUpdate();
                Package package = update.CreatePackage();

                this.Server.Send(package);
            }
        }
        #endregion
    }
}
