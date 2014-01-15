using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Game.Timing
{
    public class FrameDelayAction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameDelayAction"/> class.
        /// </summary>
        /// <param name="frameCount">The frame count.</param>
        /// <param name="action">The action.</param>
        public FrameDelayAction(int frameCount, Action action)
        {
            this.Frames = frameCount;
            this.Action = action;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the frames.
        /// </summary>
        public int Frames { get; set; }
        /// <summary>
        /// Gets the action.
        /// </summary>
        public Action Action { get; private set; }
        #endregion
    }
}
