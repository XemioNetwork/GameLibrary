using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events.Servers
{
    public class ChannelOpenedEvent : ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOpenedEvent" /> class.
        /// </summary>
        /// <param name="channel">The Channel.</param>
        public ChannelOpenedEvent(ServerChannel channel)
        {
            this.Channel = channel;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Channel.
        /// </summary>
        public ServerChannel Channel { get; private set; }
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
