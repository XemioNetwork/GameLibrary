using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JSIL;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Rendering.HTML5
{
    public class WebGameLoop : IGameLoop
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WebGameLoop"/> class.
        /// </summary>
        public WebGameLoop()
        {
        }
        #endregion

        
        #region Implementation of IGameLoop

        public void Run()
        {
        }

        public void Stop()
        {
        }

        public void Subscribe(IGameHandler handler)
        {
            var window = Builtins.Global["window"];

            window.setTimeout((Action)(() => handler.Tick((float)this.TargetTickTime)), this.TargetTickTime);
            window.setTimeout((Action)(() => handler.Render()), this.TargetFrameTime);
        }

        public void Unsubscribe(IGameHandler handler)
        {
        }

        public bool Active { get; private set; }
        public long FrameIndex { get; private set; }
        public PrecisionLevel Precision { get; set; }
        public LagCompensation LagCompensation { get; set; }
        public int FramesPerSecond { get; private set; }
        public double FrameTime { get; private set; }
        public double TickTime { get; private set; }
        public double RenderTime { get; private set; }
        public double TargetTickTime { get; set; }
        public double TargetFrameTime { get; set; }
        #endregion
    }
}
