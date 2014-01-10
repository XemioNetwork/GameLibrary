using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.GameLibrary.Game
{
    public class GameLoop : IGameLoop, IConstructable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constants
        /// <summary>
        /// The tolerance to comprehend SpinWait lag.
        /// </summary>
        public const double SpinWaitTolerance = 0.9585;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameLoop"/> class.
        /// </summary>
        public GameLoop()
        {
            this._handlers = new CachedList<IGameHandler>();

            this.Precision = PrecisionLevel.High;
            this.LagCompensation = LagCompensation.ExecuteMissedTicks;

            this.Subscribe(Frame.Instance);
        }
        #endregion

        #region Fields
        private IThreadInvoker _invoker;
        
        private Task _loopTask;
        private CancellationTokenSource _cancellationTokenSource;
        private Stopwatch _gameTime;

        private readonly CachedList<IGameHandler> _handlers;

        private double _renderTime;
        private double _tickTime;

        private bool _requestRender;
        private double _unprocessedTicks;

        private double _timeSinceLastTick;
        private double _elapsedRenderTime;

        private int _fpsCount;
        private double _lastFpsMeasure;

        private double _lastTryToTick;
        private double _lastRender;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameLoop"/> is active.
        /// </summary>
        public bool Active { get; private set; }
        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        public long FrameIndex { get; private set; }
        /// <summary>
        /// Gets or sets the precision. The precision level determines the amount of "Thread.Sleep"
        /// used to wait for the next frame. The higher the precision level, the lower the amount
        /// of Thread.Sleep used.
        /// </summary>
        public PrecisionLevel Precision { get; set; }
        /// <summary>
        /// Gets or sets the lag compensation. If set to ExecuteMissedTicks, the game loop will
        /// always run at the same tick frequency.
        /// </summary>
        public LagCompensation LagCompensation { get; set; }
        /// <summary>
        /// Gets the frames per second.
        /// </summary>
        public int FramesPerSecond { get; private set; }
        /// <summary>
        /// Gets the frame time (render time + tick time).
        /// </summary>
        public double FrameTime
        {
            get { return this._tickTime + this._renderTime; }
        }
        /// <summary>
        /// Gets the tick time.
        /// </summary>
        public double TickTime
        {
            get { return this._tickTime; }
        }
        /// <summary>
        /// Gets the render time.
        /// </summary>
        public double RenderTime
        {
            get { return this._renderTime; }
        }
        /// <summary>
        /// Gets the target frame time.
        /// </summary>
        public double TargetFrameTime { get; set; }
        #endregion
        
        #region Event Methods
        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        protected virtual void OnTick(float elapsed)
        {
            Stopwatch tickWatch = Stopwatch.StartNew();

            //Increment the frame index, since a frame has passed. A
            //frame can only pass inside a tick call, since render isn't called
            //as frequent as tick
            this.FrameIndex++;

            using (this._handlers.StartCaching())
            {
                foreach (IGameHandler gameHandler in this._handlers)
                {
                    gameHandler.Tick(elapsed);
                }
            }

            tickWatch.Stop();
            this._tickTime = tickWatch.Elapsed.TotalMilliseconds;
        }
        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        protected virtual void OnRender(float elapsed)
        {
            Stopwatch renderWatch = Stopwatch.StartNew();

            using (this._handlers.StartCaching())
            {
                foreach (IGameHandler gameHandler in this._handlers)
                {
                    gameHandler.Render();
                }
            }

            renderWatch.Stop();
            this._renderTime = renderWatch.Elapsed.TotalMilliseconds;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the game loop and creates a parallel thread.
        /// </summary>
        public void Run()
        {
            if (!this.Active)
            {
                logger.Info("Starting game loop with {0}fps.", 1000.0 / this.TargetFrameTime);

                this.Active = true;

                this._cancellationTokenSource = new CancellationTokenSource();
                this._loopTask = Task.Factory.StartNew(this.InternalLoop, this._cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
            }
        }
        /// <summary>
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            if (this.Active)
            {
                logger.Info("Terminating game loop.");

                this.Active = false;

                this._cancellationTokenSource.Cancel(false);
                this._loopTask.Wait();
            }
        }
        /// <summary>
        /// Subscribes and adds the handler to the gameloop.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Subscribe(IGameHandler handler)
        {
            logger.Debug("Subscribed {0} to the game loop.", handler.GetType().Name);
            this._handlers.Add(handler);
        }
        /// <summary>
        /// Unsubscribes the gameloop and removes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Unsubscribe(IGameHandler handler)
        {
            logger.Debug("Removed {0} from game loop.", handler.GetType().Name);
            this._handlers.Remove(handler);
        }
        /// <summary>
        /// Resets all important fields.
        /// </summary>
        private void ResetFields()
        {
            double totalMilliseconds = this._gameTime.Elapsed.TotalMilliseconds;

            this._tickTime = this._renderTime = 0;
            this._lastFpsMeasure = this._lastTryToTick = this._lastRender = totalMilliseconds;
            this._unprocessedTicks = this._elapsedRenderTime = this._timeSinceLastTick = 0;
            this._fpsCount = 0;
        }
        /// <summary>
        /// A function representing the internal game loop logic.
        /// </summary>
        private void InternalLoop()
        {
            try
            {
                this._gameTime = Stopwatch.StartNew();
                this._requestRender = true;

                this.ResetFields();
                
                while (this._cancellationTokenSource.IsCancellationRequested == false)
                {
                    //We use the SpinWait to wait for the next requested tick.
                    //It's not totally correct, but we save a LOT of CPU power.
                    //To compensate the incorrectness we introduced our SpinWaitTolerance constant.
                    SpinWait.SpinUntil(this.IsTickRequested);

                    this.CheckRenderRequest();
                    this.UpdateFramesPerSecond();

                    this.HandleRenderRequest();
                    this.HandleUnprocessedTicks();
                }
            }
            catch (Exception ex)
            {
                logger.ErrorException("Unexpected exception in game loop: ", ex);
                throw;
            }
        }
        /// <summary>
        /// Determines whether the specified time has elapsed.
        /// </summary>
        /// <param name="pointInTime">The point in time.</param>
        /// <param name="elapsed">The elapsed.</param>
        private bool IsElapsed(double pointInTime, double elapsed)
        {
            return this._gameTime.Elapsed.TotalMilliseconds - pointInTime >= elapsed;
        }
        /// <summary>
        /// Handles render requests.
        /// </summary>
        private void HandleRenderRequest()
        {
            if (this._requestRender)
            {
                this._fpsCount++;
                this._elapsedRenderTime = this._gameTime.Elapsed.TotalMilliseconds - this._lastRender;
                this._lastRender = this._gameTime.Elapsed.TotalMilliseconds;

                this._invoker.Invoke(() => this.OnRender((float)this._elapsedRenderTime));
                this._requestRender = false;

                logger.Trace("Handled render request with {0}ms frame time.", this._elapsedRenderTime);
            }
        }
        /// <summary>
        /// Handles the tick calculation.
        /// </summary>
        private bool IsTickRequested()
        {
            //Time since the last handled tick.
            double elapsed = this._gameTime.Elapsed.TotalMilliseconds - this._lastTryToTick;

            this._timeSinceLastTick += elapsed;
            this._unprocessedTicks = this._timeSinceLastTick / (this.TargetFrameTime * GameLoop.SpinWaitTolerance);
            this._lastTryToTick = this._gameTime.Elapsed.TotalMilliseconds;

            bool shouldTick = this._unprocessedTicks >= 1.0;
            
            return shouldTick;
        }
        /// <summary>
        /// Handles all unprocessed ticks.
        /// </summary>
        private void HandleUnprocessedTicks()
        {
            //If there are unprocessed ticks, call OnTick.
            if (this._unprocessedTicks >= 1)
            {
                int tickCount = (int)this._unprocessedTicks;

                //Subtract unprocessed ticks as an integer, since we want
                //to keep digits for the next tick. (Example: unprocessedTicks = 3.11, => tickCount = 3)
                this._unprocessedTicks -= tickCount;

                logger.Trace("Game tick elapsed with {0}ms", this._timeSinceLastTick);

                switch (this.LagCompensation)
                {
                    case LagCompensation.None:
                        this._invoker.Invoke(() => this.OnTick((float)this._timeSinceLastTick));
                        break;
                    case LagCompensation.ExecuteMissedTicks:
                        //Handle unprocessed ticks by calling the OnTick method as often
                        //as needed for the elapsed tick time.
                        //Example: elapsedTickTime = 32ms, TargetFrameTime = 16ms => call OnTick twice.

                        float tickElapsed = (float)(this._timeSinceLastTick / tickCount);
                        for (int i = 0; i < tickCount; i++)
                        {
                            this._invoker.Invoke(() => this.OnTick(tickElapsed));
                        }
                        break;
                }

                this._timeSinceLastTick = 0;
                this.ManagePrecisionLevel();
            }
        }
        /// <summary>
        /// Checks for render requests.
        /// </summary>
        private void CheckRenderRequest()
        {
            if (this.IsElapsed(this._lastRender, this.TargetFrameTime * GameLoop.SpinWaitTolerance))
            {
                this._requestRender = true;
            }
        }
        /// <summary>
        /// Updates the timing properties for FramesPerSecond.
        /// </summary>
        private void UpdateFramesPerSecond()
        {
            if (this.IsElapsed(this._lastFpsMeasure, 1000.0))
            {
                this.FramesPerSecond = this._fpsCount;

                logger.Debug("GameLoop running with {0} fps", this.FramesPerSecond);

                this._lastFpsMeasure = this._gameTime.Elapsed.TotalMilliseconds;
                this._fpsCount = 0;
            }
        }
        /// <summary>
        /// Manages the precision level.
        /// </summary>
        private void ManagePrecisionLevel()
        {
            double frameTime = this._renderTime + this._tickTime;
            int timeTillNextFrame = (int)(this.TargetFrameTime - frameTime);

            if (timeTillNextFrame < 0)
                return;

            switch (this.Precision)
            {
                case PrecisionLevel.Highest:
                    //Use busy waiting for the game loop
                    break;
                case PrecisionLevel.High:
                    Thread.Sleep(timeTillNextFrame / 8);
                    break;
                case PrecisionLevel.Normal:
                    Thread.Sleep(timeTillNextFrame / 4);
                    break;
                case PrecisionLevel.Low:
                    Thread.Sleep(timeTillNextFrame / 2);
                    break;
                case PrecisionLevel.Lowest:
                    Thread.Sleep(timeTillNextFrame);
                    break;
            }
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            this._invoker = XGL.Components.Get<IThreadInvoker>();
        }
        #endregion
    }
}
