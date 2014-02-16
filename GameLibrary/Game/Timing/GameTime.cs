using System;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;

namespace Xemio.GameLibrary.Game.Timing
{
    [Require(typeof(IGameLoop))]

    public class GameTime : IConstructable, ITickHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameTime"/> class.
        /// </summary>
        public GameTime()
        {
            this.TimeMode = GameTimeMode.Simulated;
            this.DayLength = TimeSpan.FromMinutes(8);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the time.
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Gets or sets the time mode.
        /// </summary>
        public GameTimeMode TimeMode { get; set; }
        /// <summary>
        /// Gets or sets length for one complete day cycle ingame. (e.g. real-time would be 24h)
        /// </summary>
        public TimeSpan DayLength { get; set; }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            var loop = XGL.Components.Get<IGameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Implementation of IGameHandler
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            switch (this.TimeMode)
            {
                case GameTimeMode.RealTimeClock:
                    this.Time = DateTime.Now;
                    break;
                case GameTimeMode.Simulated:
                    this.Time += TimeSpan.FromMilliseconds(elapsed / this.DayLength.TotalMilliseconds);
                    break;
            }
        }
        #endregion
    }
}
