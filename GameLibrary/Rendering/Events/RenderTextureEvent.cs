using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Events
{
    public class RenderTextureEvent : ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderTextureEvent"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public RenderTextureEvent(ITexture texture, Rectangle destination, Rectangle origin)
        {
            this.Texture = texture;
            this.Destination = destination;
            this.Origin = origin;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the texture.
        /// </summary>
        public ITexture Texture { get; set; }
        /// <summary>
        /// Gets the destination.
        /// </summary>
        public Rectangle Destination { get; set; }
        /// <summary>
        /// Gets the origin.
        /// </summary>
        public Rectangle Origin { get; set; }
        #endregion

        #region Implementation of ICancelableEvent
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
