using System;
using NLog;
using Xemio.GameLibrary.Common.Threads;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Events.Servers;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Dispatchers
{
    internal class ServerPackageDispatcher : Worker
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly ServerChannel _channel;
        private readonly IThreadInvoker _threadInvoker;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerPackageDispatcher" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public ServerPackageDispatcher(ServerChannel channel) : base(ThreadStartBehavior.AutoStart)
        {
            this._channel = channel;
            this._threadInvoker = XGL.Components.Get<IThreadInvoker>();
        }
        #endregion

        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            try
            {
                var eventManager = XGL.Components.Require<IEventManager>();

                while (this._channel.Connected)
                {
                    Package package = this._channel.Receive();
                    if (package != null)
                    {
                        logger.Trace("Received {0} from {1}.", package.GetType().Name, this._channel.Address);
                        eventManager.Publish(new ServerReceivedPackageEvent(package, this._channel));
                    }
                }
            }
            catch (ChannelLostConnectionException ex) { }
            catch (Exception ex)
            {
                logger.ErrorException(string.Format("Error while receiving package from {0}.", this._channel.Address), ex);
            }
            finally
            {
                var eventManager = XGL.Components.Require<IEventManager>();
                eventManager.Publish(new ChannelClosedEvent(this._channel));
            }
        }
        #endregion
    }
}
