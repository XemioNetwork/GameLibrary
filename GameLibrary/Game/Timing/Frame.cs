using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Game.Handlers;

namespace Xemio.GameLibrary.Game.Timing
{
    public class Frame : ITickHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        public Frame()
        {
            this._actions = new List<FrameDelayAction>();
        }
        #endregion

        #region Fields
        private readonly List<FrameDelayAction> _actions; 
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Add(FrameDelayAction action)
        {
            this._actions.Add(action);
        }
        #endregion

        #region Implementation of IGameHandler
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            var removedActions = new List<FrameDelayAction>();

            foreach (FrameDelayAction action in this._actions)
            {
                action.Frames--;
                if (action.Frames <= 0)
                {
                    action.Action();
                    removedActions.Add(action);
                }
            }

            foreach (FrameDelayAction action in removedActions)
            {
                this._actions.Remove(action);
            }
        }
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        internal static Frame Instance
        {
            get { return Singleton<Frame>.Value; }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Delays the specified action for one frame.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Delay(Action action)
        {
            Frame.Delay(1, action);
        }
        /// <summary>
        /// Delays the specified action for x frames.
        /// </summary>
        /// <param name="frameCount">The frame count.</param>
        /// <param name="action">The action.</param>
        public static void Delay(int frameCount, Action action)
        {
            Frame.Instance.Add(new FrameDelayAction(frameCount, action));
        }
        #endregion
    }
}
